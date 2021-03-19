namespace MMO_NETWORK_LAUNCHER
{
    partial class PreLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreLoading));
            this.version = new System.Windows.Forms.Label();
            this.version_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.BackColor = System.Drawing.Color.Transparent;
            this.version.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.version.Location = new System.Drawing.Point(385, 121);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(48, 16);
            this.version.TabIndex = 4;
            this.version.Text = "0.0.0.0";
            // 
            // version_text
            // 
            this.version_text.AutoSize = true;
            this.version_text.BackColor = System.Drawing.Color.Transparent;
            this.version_text.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.version_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
            this.version_text.Location = new System.Drawing.Point(330, 121);
            this.version_text.Name = "version_text";
            this.version_text.Size = new System.Drawing.Size(49, 16);
            this.version_text.TabIndex = 5;
            this.version_text.Text = "Версия";
            // 
            // PreLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::MMO_NETWORK_LAUNCHER.Properties.Resources.launcher_preload;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(445, 148);
            this.ControlBox = false;
            this.Controls.Add(this.version_text);
            this.Controls.Add(this.version);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreLoading";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка... LG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label version_text;

    }
}