namespace EncodeDecodeConfiguration
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox = new System.Windows.Forms.TextBox();
            this.Encode = new System.Windows.Forms.Button();
            this.Decode = new System.Windows.Forms.Button();
            this.LoadFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_1 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.int_1 = new System.Windows.Forms.Label();
            this.int_2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SaveFile = new System.Windows.Forms.Button();
            this.NewDecript = new System.Windows.Forms.Button();
            this.NewEncript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(1, 1);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(499, 174);
            this.textBox.TabIndex = 0;
            this.textBox.Text = "LauncherGameCript$rev1.0.1.5$\r\n[server]$\r\nAuth=darkteam.zapto.org$\r\nweb=darkteam." +
                "zapto.org$\r\npath=LG$";
            // 
            // Encode
            // 
            this.Encode.Location = new System.Drawing.Point(12, 181);
            this.Encode.Name = "Encode";
            this.Encode.Size = new System.Drawing.Size(80, 22);
            this.Encode.TabIndex = 1;
            this.Encode.Text = "Encode";
            this.Encode.UseVisualStyleBackColor = true;
            this.Encode.Click += new System.EventHandler(this.Encode_Click);
            // 
            // Decode
            // 
            this.Decode.Location = new System.Drawing.Point(12, 205);
            this.Decode.Name = "Decode";
            this.Decode.Size = new System.Drawing.Size(80, 22);
            this.Decode.TabIndex = 2;
            this.Decode.Text = "Decode";
            this.Decode.UseVisualStyleBackColor = true;
            this.Decode.Click += new System.EventHandler(this.Decode_Click);
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(116, 181);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(80, 22);
            this.LoadFile.TabIndex = 3;
            this.LoadFile.Text = "LoadFile";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "\"LauncherGame Configuration\"|*.ini";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.ClickDialogOK);
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(317, 192);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(93, 13);
            this.label_1.TabIndex = 4;
            this.label_1.Text = "Прочитано байт: ";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(328, 205);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(82, 13);
            this.label_2.TabIndex = 5;
            this.label_2.Text = "Версия файла:";
            // 
            // int_1
            // 
            this.int_1.AutoSize = true;
            this.int_1.Location = new System.Drawing.Point(427, 192);
            this.int_1.Name = "int_1";
            this.int_1.Size = new System.Drawing.Size(13, 13);
            this.int_1.TabIndex = 6;
            this.int_1.Text = "0";
            // 
            // int_2
            // 
            this.int_2.AutoSize = true;
            this.int_2.Location = new System.Drawing.Point(427, 205);
            this.int_2.Name = "int_2";
            this.int_2.Size = new System.Drawing.Size(13, 13);
            this.int_2.TabIndex = 7;
            this.int_2.Text = "0";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1, 233);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(499, 71);
            this.textBox1.TabIndex = 8;
            // 
            // SaveFile
            // 
            this.SaveFile.Location = new System.Drawing.Point(116, 205);
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(80, 22);
            this.SaveFile.TabIndex = 9;
            this.SaveFile.Text = "SaveFile";
            this.SaveFile.UseVisualStyleBackColor = true;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // NewDecript
            // 
            this.NewDecript.Location = new System.Drawing.Point(215, 180);
            this.NewDecript.Name = "NewDecript";
            this.NewDecript.Size = new System.Drawing.Size(75, 23);
            this.NewDecript.TabIndex = 10;
            this.NewDecript.Text = "NewDecript";
            this.NewDecript.UseVisualStyleBackColor = true;
            this.NewDecript.Click += new System.EventHandler(this.NewDecript_Click);
            // 
            // NewEncript
            // 
            this.NewEncript.Location = new System.Drawing.Point(215, 205);
            this.NewEncript.Name = "NewEncript";
            this.NewEncript.Size = new System.Drawing.Size(75, 23);
            this.NewEncript.TabIndex = 11;
            this.NewEncript.Text = "NewEncript";
            this.NewEncript.UseVisualStyleBackColor = true;
            this.NewEncript.Click += new System.EventHandler(this.NewEncript_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 316);
            this.Controls.Add(this.NewEncript);
            this.Controls.Add(this.NewDecript);
            this.Controls.Add(this.SaveFile);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.int_2);
            this.Controls.Add(this.int_1);
            this.Controls.Add(this.label_2);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.LoadFile);
            this.Controls.Add(this.Decode);
            this.Controls.Add(this.Encode);
            this.Controls.Add(this.textBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Encode;
        private System.Windows.Forms.Button Decode;
        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Label int_1;
        private System.Windows.Forms.Label int_2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SaveFile;
        private System.Windows.Forms.Button NewDecript;
        private System.Windows.Forms.Button NewEncript;
    }
}