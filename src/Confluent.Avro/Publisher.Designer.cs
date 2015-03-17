namespace Confluent.Avro
{
    partial class Publisher
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.buttonPublishWithSchema = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNumberOfMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Topic:";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(128, 8);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(220, 20);
            this.textBoxTopic.TabIndex = 4;
            this.textBoxTopic.Text = "test";
            // 
            // buttonPublishWithSchema
            // 
            this.buttonPublishWithSchema.Location = new System.Drawing.Point(354, 6);
            this.buttonPublishWithSchema.Name = "buttonPublishWithSchema";
            this.buttonPublishWithSchema.Size = new System.Drawing.Size(75, 46);
            this.buttonPublishWithSchema.TabIndex = 6;
            this.buttonPublishWithSchema.Text = "Publish with schema";
            this.buttonPublishWithSchema.UseVisualStyleBackColor = true;
            this.buttonPublishWithSchema.Click += new System.EventHandler(this.buttonPublishWithSchema_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessages.Location = new System.Drawing.Point(12, 58);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessages.Size = new System.Drawing.Size(598, 281);
            this.textBoxMessages.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number Of Messages:";
            // 
            // textBoxNumberOfMsg
            // 
            this.textBoxNumberOfMsg.Location = new System.Drawing.Point(128, 32);
            this.textBoxNumberOfMsg.Name = "textBoxNumberOfMsg";
            this.textBoxNumberOfMsg.Size = new System.Drawing.Size(220, 20);
            this.textBoxNumberOfMsg.TabIndex = 8;
            this.textBoxNumberOfMsg.Text = "1";
            // 
            // Publisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 351);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNumberOfMsg);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonPublishWithSchema);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTopic);
            this.Name = "Publisher";
            this.Text = "Publisher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.Button buttonPublishWithSchema;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNumberOfMsg;
    }
}