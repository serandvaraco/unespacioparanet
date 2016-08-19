namespace WindowsIdentityFoundation
{
    partial class Identityfrm
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
            this.btnSend = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.btnSendAction = new System.Windows.Forms.Button();
            this.btnClaimTypes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(187, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(13, 13);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(168, 22);
            this.txtValue.TabIndex = 1;
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 16;
            this.lstResult.Location = new System.Drawing.Point(13, 42);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(490, 84);
            this.lstResult.TabIndex = 2;
            // 
            // btnSendAction
            // 
            this.btnSendAction.Location = new System.Drawing.Point(268, 12);
            this.btnSendAction.Name = "btnSendAction";
            this.btnSendAction.Size = new System.Drawing.Size(114, 23);
            this.btnSendAction.TabIndex = 3;
            this.btnSendAction.Text = "Send Action";
            this.btnSendAction.UseVisualStyleBackColor = true;
            this.btnSendAction.Click += new System.EventHandler(this.btnSendAction_Click);
            // 
            // btnClaimTypes
            // 
            this.btnClaimTypes.Location = new System.Drawing.Point(389, 13);
            this.btnClaimTypes.Name = "btnClaimTypes";
            this.btnClaimTypes.Size = new System.Drawing.Size(114, 23);
            this.btnClaimTypes.TabIndex = 4;
            this.btnClaimTypes.Text = "Claim Types";
            this.btnClaimTypes.UseVisualStyleBackColor = true;
            this.btnClaimTypes.Click += new System.EventHandler(this.btnClaimTypes_Click);
            // 
            // Identityfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 132);
            this.Controls.Add(this.btnClaimTypes);
            this.Controls.Add(this.btnSendAction);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnSend);
            this.Name = "Identityfrm";
            this.Text = "Windows Identity Foundation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnSendAction;
        private System.Windows.Forms.Button btnClaimTypes;
    }
}

