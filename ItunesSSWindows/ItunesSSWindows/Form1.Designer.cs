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
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbStatus
            // 
            this.txbStatus.Location = new System.Drawing.Point(25, 33);
            this.txbStatus.Multiline = true;
            this.txbStatus.Name = "txbStatus";
            this.txbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
            this.txbUserName.Location = new System.Drawing.Point(158, 160);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(372, 20);
            this.txbUserName.TabIndex = 2;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(158, 186);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(371, 20);
            this.txbPassword.TabIndex = 3;
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Location = new System.Drawing.Point(25, 166);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(127, 13);
            this.lblEmailAddress.TabIndex = 5;
            this.lblEmailAddress.Text = "Registered Email Address";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(25, 192);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(107, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Registered Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Version 0.1.1.0 (Build 7) - PRE RELEASE ALPHA\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "18th March 2012";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 285);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblEmailAddress);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbUserName);
            this.Controls.Add(this.btnRunStats);
            this.Controls.Add(this.txbStatus);
            this.Name = "Form1";
            this.Text = "Itunes Stats Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbStatus;
        private System.Windows.Forms.Button btnRunStats;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label lblEmailAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

