namespace File_Sharing_Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.upload_btn = new System.Windows.Forms.Button();
            this.openFile_btn = new System.Windows.Forms.Button();
            this.download_btn = new System.Windows.Forms.Button();
            this.openFileDir_txtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(413, 20);
            this.textBox1.TabIndex = 0;
            // 
            // upload_btn
            // 
            this.upload_btn.Location = new System.Drawing.Point(510, 13);
            this.upload_btn.Name = "upload_btn";
            this.upload_btn.Size = new System.Drawing.Size(75, 23);
            this.upload_btn.TabIndex = 1;
            this.upload_btn.Text = "Send";
            this.upload_btn.UseVisualStyleBackColor = true;
            this.upload_btn.Click += new System.EventHandler(this.upload_btn_Click);
            // 
            // openFile_btn
            // 
            this.openFile_btn.Location = new System.Drawing.Point(431, 13);
            this.openFile_btn.Name = "openFile_btn";
            this.openFile_btn.Size = new System.Drawing.Size(75, 23);
            this.openFile_btn.TabIndex = 3;
            this.openFile_btn.Text = "Open File";
            this.openFile_btn.UseVisualStyleBackColor = true;
            this.openFile_btn.Click += new System.EventHandler(this.openFile_btn_Click);
            // 
            // download_btn
            // 
            this.download_btn.Location = new System.Drawing.Point(431, 45);
            this.download_btn.Name = "download_btn";
            this.download_btn.Size = new System.Drawing.Size(75, 23);
            this.download_btn.TabIndex = 4;
            this.download_btn.Text = "Download";
            this.download_btn.UseVisualStyleBackColor = true;
            this.download_btn.Click += new System.EventHandler(this.download_btn_Click);
            // 
            // openFileDir_txtBox
            // 
            this.openFileDir_txtBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.openFileDir_txtBox.Location = new System.Drawing.Point(12, 15);
            this.openFileDir_txtBox.Name = "openFileDir_txtBox";
            this.openFileDir_txtBox.ReadOnly = true;
            this.openFileDir_txtBox.Size = new System.Drawing.Size(413, 20);
            this.openFileDir_txtBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 82);
            this.Controls.Add(this.openFileDir_txtBox);
            this.Controls.Add(this.download_btn);
            this.Controls.Add(this.openFile_btn);
            this.Controls.Add(this.upload_btn);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button upload_btn;
        private System.Windows.Forms.Button openFile_btn;
        private System.Windows.Forms.Button download_btn;
        private System.Windows.Forms.TextBox openFileDir_txtBox;
    }
}

