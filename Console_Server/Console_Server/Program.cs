using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Console_Server
{
    class Program
    {
        static List<String> filesOnServer = new List<string>();
        static string filesPath = AppDomain.CurrentDomain.BaseDirectory + "/Files";
        static byte[] serverData = null;

        static void Main(string[] args)
        {
            openFile();

            Console.WriteLine("Files on server: ");
            foreach (string s in filesOnServer)
                Console.WriteLine(s);

            TcpListener server = null;

            try
            {
                int MaxThreadsCount = Environment.ProcessorCount * 4;
                ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
                ThreadPool.SetMinThreads(2, 2);

                Int32 port = 9595;
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                int counter = 0;

                server = new TcpListener(ip, port);
                server.Start();

                while (true)
                {
                    Console.Write("\nWaiting for a connection... ");

                    ThreadPool.QueueUserWorkItem(processing, server.AcceptTcpClient());
                    counter++;
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        static void processing(object client_obj)
        {
            Byte[] buffer = new Byte[100000];
            TcpClient client = client_obj as TcpClient;
            NetworkStream stream = client.GetStream();

            stream.Read(buffer, 0, buffer.Length);

            string command = Encoding.ASCII.GetString(buffer, 0, 1);
            int fileNameLength = 0;
            string fileName = string.Empty;

            switch (command)
            {
                case "1":
                    fileNameLength = BitConverter.ToInt32(buffer, 1);
                    fileName = Encoding.ASCII.GetString(buffer, 5, fileNameLength);

                    foreach (string s in filesOnServer)
                    {
                        if (s.ToLower() == fileName.ToLower())
                        {
                            Console.WriteLine("create packege...");
                            createPackage(fileName);
                            Console.WriteLine("send file...");
                            stream.Write(serverData, 0, serverData.Length);
                            break;
                        }
                    }
                    break;

                case "2":
                    int size = (int)BitConverter.ToInt64(buffer, 1);
                    fileNameLength = BitConverter.ToInt32(buffer, 9);
                    fileName = Encoding.ASCII.GetString(buffer, 13, fileNameLength);
                    File.AppendAllText("Files.txt", "\n" + fileName);
                    BinaryWriter write = new BinaryWriter(File.Open(filesPath + "/" + fileName, FileMode.Append));
                    write.Write(buffer, 13 + fileNameLength, size - 13 - fileNameLength);
                    write.Close();
                    Console.WriteLine("File received");
                    break;
                default:
                    break;
            }

            stream.Close();
            client.Close();
        }

        public static void createPackage(string fileName)
        {
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileNameLength = BitConverter.GetBytes(fileNameByte.Length);
            byte[] fileData = File.ReadAllBytes(filesPath + "/" + fileName);
                
            int size = 4 + fileNameByte.Length + fileData.Length;
            byte[] sizeByte = BitConverter.GetBytes(size);

            serverData = new byte[size + 8];
            
            sizeByte.CopyTo(serverData, 0);
            fileNameLength.CopyTo(serverData, 8);
            fileNameByte.CopyTo(serverData, 12);
            fileData.CopyTo(serverData, 12 + fileNameByte.Length);
        }

        private static void openFile()
        {
            string line = string.Empty;

            try
            {
                using (StreamReader sr = new StreamReader("Files.txt"))
                {
                    while ((line = sr.ReadLine()) != null) filesOnServer.Add(line);
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
