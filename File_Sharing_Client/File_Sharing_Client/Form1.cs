using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace File_Sharing_Client
{
    public partial class Form1 : Form
    {
        FileDialog openFile = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void upload_btn_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient("127.0.0.1", 9595);
            NetworkStream stream = client.GetStream();

            byte[] uploadData = uploadFilePackage("2", openFileDir_txtBox.Text);
            stream.Write(uploadData, 0, uploadData.Length);

            stream.Close();
            client.Close();
        }

        private void openFile_btn_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
                openFileDir_txtBox.Text = openFile.FileName;
        }

        private void download_btn_Click(object sender, EventArgs e)
        {
            byte[] command = downloadCommand("1", textBox1.Text);
            connect("127.0.0.1", command);
        }

        public byte[] uploadFilePackage(string command, string fileDir)
        {
            string filePath = string.Empty;
            string fileName = fileDir.Replace("\\", "/");

            while (fileName.IndexOf("/") > -1)
            {
                filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                fileName = fileName.Substring(fileName.IndexOf("/") + 1);
            }

            byte[] commandByte = Encoding.ASCII.GetBytes(command);
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileNameLength = BitConverter.GetBytes(fileNameByte.Length);
            byte[] fileData = File.ReadAllBytes(filePath + fileName);

            int size = 5 + fileNameByte.Length + fileData.Length;
            byte[] sizeByte = BitConverter.GetBytes(size);

            byte[] uploadData = new byte[size + 8];

            commandByte.CopyTo(uploadData, 0);
            sizeByte.CopyTo(uploadData, 1);
            fileNameLength.CopyTo(uploadData, 9);
            fileNameByte.CopyTo(uploadData, 13);
            fileData.CopyTo(uploadData, 13 + fileNameByte.Length);

            return uploadData;
        }

        public byte[] downloadCommand(string command, string fileName)
        {
            byte[] commandByte = Encoding.ASCII.GetBytes(command);
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fNameLen = BitConverter.GetBytes(fileNameByte.Length);

            byte[] downloadData = new byte[5 + fileNameByte.Length];

            commandByte.CopyTo(downloadData, 0);
            fNameLen.CopyTo(downloadData, 1);
            fileNameByte.CopyTo(downloadData, 5);

            return downloadData;
        }

        public void connect(string server, byte[] message)
        {
            try
            {
                TcpClient client = new TcpClient(server, 9595);

                NetworkStream stream = client.GetStream();
                stream.Write(message, 0, message.Length);

                Byte[] buffer = new Byte[100000];

                stream.Read(buffer, 0, buffer.Length);

                int size = (int)BitConverter.ToInt64(buffer, 0);
                int fileNameLength = BitConverter.ToInt32(buffer, 8);
                string fileName = Encoding.ASCII.GetString(buffer, 12, fileNameLength);

                BinaryWriter write = new BinaryWriter(File.Open(Application.StartupPath + @"\" + fileName, FileMode.Append));
                write.Write(buffer, 12 + fileNameLength, buffer.Length - 12 - fileNameLength);

                write.Close();
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
        }
    }
}
