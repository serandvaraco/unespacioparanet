namespace MicrosoftAzureRedisCache
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnGetObjects = new System.Windows.Forms.Button();
            this.btnSendObjects = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 49);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(169, 13);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(151, 49);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "Get Information";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 16;
            this.lstResult.Location = new System.Drawing.Point(13, 69);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(307, 212);
            this.lstResult.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(327, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(385, 212);
            this.dataGridView1.TabIndex = 3;
            // 
            // btnGetObjects
            // 
            this.btnGetObjects.Location = new System.Drawing.Point(531, 14);
            this.btnGetObjects.Name = "btnGetObjects";
            this.btnGetObjects.Size = new System.Drawing.Size(151, 49);
            this.btnGetObjects.TabIndex = 4;
            this.btnGetObjects.Text = "Get Information Object";
            this.btnGetObjects.UseVisualStyleBackColor = true;
            this.btnGetObjects.Click += new System.EventHandler(this.btnGetObjects_Click);
            // 
            // btnSendObjects
            // 
            this.btnSendObjects.Location = new System.Drawing.Point(375, 14);
            this.btnSendObjects.Name = "btnSendObjects";
            this.btnSendObjects.Size = new System.Drawing.Size(150, 49);
            this.btnSendObjects.TabIndex = 5;
            this.btnSendObjects.Text = "Save Object";
            this.btnSendObjects.UseVisualStyleBackColor = true;
            this.btnSendObjects.Click += new System.EventHandler(this.btnSendObjects_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 286);
            this.Controls.Add(this.btnSendObjects);
            this.Controls.Add(this.btnGetObjects);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnSave);
            this.Name = "Form1";
            this.Text = "Caché en Redis";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnGetObjects;
        private System.Windows.Forms.Button btnSendObjects;
    }
}

