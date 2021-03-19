namespace MMO_NETWORK_LAUNCHER.src.windows
{
    partial class UserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInfo));
            this.UserNameText = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.UserEmailText = new System.Windows.Forms.Label();
            this.UserAgeText = new System.Windows.Forms.Label();
            this.UserEmail = new System.Windows.Forms.Label();
            this.UserAge = new System.Windows.Forms.Label();
            this.CloseUserInfo = new System.Windows.Forms.Button();
            this.MoneyText = new System.Windows.Forms.Label();
            this.Money = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserNameText
            // 
            this.UserNameText.AutoSize = true;
            this.UserNameText.Location = new System.Drawing.Point(21, 9);
            this.UserNameText.Name = "UserNameText";
            this.UserNameText.Size = new System.Drawing.Size(63, 13);
            this.UserNameText.TabIndex = 0;
            this.UserNameText.Text = "UserName: ";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.UserName.Location = new System.Drawing.Point(87, 9);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(48, 13);
            this.UserName.TabIndex = 1;
            this.UserName.Text = "nullstring";
            // 
            // UserEmailText
            // 
            this.UserEmailText.AutoSize = true;
            this.UserEmailText.Location = new System.Drawing.Point(21, 32);
            this.UserEmailText.Name = "UserEmailText";
            this.UserEmailText.Size = new System.Drawing.Size(38, 13);
            this.UserEmailText.TabIndex = 2;
            this.UserEmailText.Text = "Email: ";
            // 
            // UserAgeText
            // 
            this.UserAgeText.AutoSize = true;
            this.UserAgeText.Location = new System.Drawing.Point(21, 57);
            this.UserAgeText.Name = "UserAgeText";
            this.UserAgeText.Size = new System.Drawing.Size(32, 13);
            this.UserAgeText.TabIndex = 3;
            this.UserAgeText.Text = "Age: ";
            // 
            // UserEmail
            // 
            this.UserEmail.AutoSize = true;
            this.UserEmail.Location = new System.Drawing.Point(87, 32);
            this.UserEmail.Name = "UserEmail";
            this.UserEmail.Size = new System.Drawing.Size(48, 13);
            this.UserEmail.TabIndex = 4;
            this.UserEmail.Text = "nullstring";
            // 
            // UserAge
            // 
            this.UserAge.AutoSize = true;
            this.UserAge.Location = new System.Drawing.Point(87, 57);
            this.UserAge.Name = "UserAge";
            this.UserAge.Size = new System.Drawing.Size(48, 13);
            this.UserAge.TabIndex = 5;
            this.UserAge.Text = "nullstring";
            // 
            // CloseUserInfo
            // 
            this.CloseUserInfo.Location = new System.Drawing.Point(236, 9);
            this.CloseUserInfo.Name = "CloseUserInfo";
            this.CloseUserInfo.Size = new System.Drawing.Size(75, 23);
            this.CloseUserInfo.TabIndex = 6;
            this.CloseUserInfo.Text = "Close";
            this.CloseUserInfo.UseVisualStyleBackColor = true;
            this.CloseUserInfo.Click += new System.EventHandler(this.CloseUserInfo_Click);
            // 
            // MoneyText
            // 
            this.MoneyText.AutoSize = true;
            this.MoneyText.Location = new System.Drawing.Point(21, 79);
            this.MoneyText.Name = "MoneyText";
            this.MoneyText.Size = new System.Drawing.Size(45, 13);
            this.MoneyText.TabIndex = 7;
            this.MoneyText.Text = "Money: ";
            // 
            // Money
            // 
            this.Money.AutoSize = true;
            this.Money.Location = new System.Drawing.Point(87, 79);
            this.Money.Name = "Money";
            this.Money.Size = new System.Drawing.Size(48, 13);
            this.Money.TabIndex = 8;
            this.Money.Text = "nullstring";
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 110);
            this.Controls.Add(this.Money);
            this.Controls.Add(this.MoneyText);
            this.Controls.Add(this.CloseUserInfo);
            this.Controls.Add(this.UserAge);
            this.Controls.Add(this.UserEmail);
            this.Controls.Add(this.UserAgeText);
            this.Controls.Add(this.UserEmailText);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.UserNameText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserInfo";
            this.Text = "User Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameText;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label UserEmailText;
        private System.Windows.Forms.Label UserAgeText;
        private System.Windows.Forms.Label UserEmail;
        private System.Windows.Forms.Label UserAge;
        private System.Windows.Forms.Button CloseUserInfo;
        private System.Windows.Forms.Label MoneyText;
        private System.Windows.Forms.Label Money;
    }
}