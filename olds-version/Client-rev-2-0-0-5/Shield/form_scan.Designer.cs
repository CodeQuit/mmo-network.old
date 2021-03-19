namespace Shield
{
    partial class form_scan
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
            this.Status_lang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Status_lang
            // 
            this.Status_lang.Location = new System.Drawing.Point(94, 25);
            this.Status_lang.Name = "Status_lang";
            this.Status_lang.Size = new System.Drawing.Size(155, 17);
            this.Status_lang.TabIndex = 0;
            this.Status_lang.Text = "PRE INIT >>>";
            // 
            // form_scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 64);
            this.Controls.Add(this.Status_lang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_scan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_scan";
            this.Load += new System.EventHandler(this.form_scan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Status_lang;
    }
}