namespace ItunesSSWindows
{
    partial class Form1
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
            this.txbStatus = new System.Windows.Forms.TextBox();
            this.btnRunStats = new System.Windows.Forms.Button();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.btnDeveloper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbStatus
            // 
            this.txbStatus.Location = new System.Drawing.Point(25, 33);
            this.txbStatus.Multiline = true;
            this.txbStatus.Name = "txbStatus";
            this.txbStatus.Size = new System.Drawing.Size(505, 121);
            this.txbStatus.TabIndex = 0;
            // 
            // btnRunStats
            // 
            this.btnRunStats.Location = new System.Drawing.Point(454, 238);
            this.btnRunStats.Name = "btnRunStats";
            this.btnRunStats.Size = new System.Drawing.Size(75, 23);
            this.btnRunStats.TabIndex = 1;
            this.btnRunStats.Text = "Run";
            this.btnRunStats.UseVisualStyleBackColor = true;
            this.btnRunStats.Click += new System.EventHandler(this.btnRunStats_Click);
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(142, 171);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(100, 20);
            this.txbUserName.TabIndex = 2;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(142, 209);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(100, 20);
            this.txbPassword.TabIndex = 3;
            // 
            // btnDeveloper
            // 
            this.btnDeveloper.Location = new System.Drawing.Point(321, 237);
            this.btnDeveloper.Name = "btnDeveloper";
            this.btnDeveloper.Size = new System.Drawing.Size(101, 23);
            this.btnDeveloper.TabIndex = 4;
            this.btnDeveloper.Text = "DeveloperOnly";
            this.btnDeveloper.UseVisualStyleBackColor = true;
            this.btnDeveloper.Click += new System.EventHandler(this.btnDeveloper_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 285);
            this.Controls.Add(this.btnDeveloper);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbUserName);
            this.Controls.Add(this.btnRunStats);
            this.Controls.Add(this.txbStatus);
            this.Name = "Form1";
            this.Text = "ItunesSS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbStatus;
        private System.Windows.Forms.Button btnRunStats;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Button btnDeveloper;
    }
}

