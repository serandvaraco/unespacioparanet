namespace AzureStorage
{
    partial class QueueForm
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
            this.btnSendQueue = new System.Windows.Forms.Button();
            this.btnGetQueue = new System.Windows.Forms.Button();
            this.btnUpdateQueue = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSendQueue
            // 
            this.btnSendQueue.Location = new System.Drawing.Point(17, 16);
            this.btnSendQueue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendQueue.Name = "btnSendQueue";
            this.btnSendQueue.Size = new System.Drawing.Size(100, 28);
            this.btnSendQueue.TabIndex = 0;
            this.btnSendQueue.Text = "Send";
            this.btnSendQueue.UseVisualStyleBackColor = true;
            this.btnSendQueue.Click += new System.EventHandler(this.btnSendQueue_Click);
            // 
            // btnGetQueue
            // 
            this.btnGetQueue.Location = new System.Drawing.Point(127, 15);
            this.btnGetQueue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetQueue.Name = "btnGetQueue";
            this.btnGetQueue.Size = new System.Drawing.Size(100, 28);
            this.btnGetQueue.TabIndex = 1;
            this.btnGetQueue.Text = "Get";
            this.btnGetQueue.UseVisualStyleBackColor = true;
            this.btnGetQueue.Click += new System.EventHandler(this.btnGetQueue_Click);
            // 
            // btnUpdateQueue
            // 
            this.btnUpdateQueue.Location = new System.Drawing.Point(235, 15);
            this.btnUpdateQueue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdateQueue.Name = "btnUpdateQueue";
            this.btnUpdateQueue.Size = new System.Drawing.Size(100, 28);
            this.btnUpdateQueue.TabIndex = 2;
            this.btnUpdateQueue.Text = "Update";
            this.btnUpdateQueue.UseVisualStyleBackColor = true;
            this.btnUpdateQueue.Click += new System.EventHandler(this.btnUpdateQueue_Click);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.ItemHeight = 16;
            this.lstResults.Location = new System.Drawing.Point(17, 53);
            this.lstResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(426, 244);
            this.lstResults.TabIndex = 3;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(343, 16);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 28);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // QueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 308);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnUpdateQueue);
            this.Controls.Add(this.btnGetQueue);
            this.Controls.Add(this.btnSendQueue);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "QueueForm";
            this.Text = "QueueForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSendQueue;
        private System.Windows.Forms.Button btnGetQueue;
        private System.Windows.Forms.Button btnUpdateQueue;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Button btnRemove;
    }
}