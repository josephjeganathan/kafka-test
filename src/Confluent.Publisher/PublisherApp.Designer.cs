namespace Confluent.Publisher
{
    partial class PublisherApp
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
            this.textBoxNumberOfMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPublish = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.buttonCreateConsumer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Topic:";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(130, 6);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(220, 20);
            this.textBoxTopic.TabIndex = 2;
            this.textBoxTopic.Text = "test";
            // 
            // textBoxNumberOfMsg
            // 
            this.textBoxNumberOfMsg.Location = new System.Drawing.Point(130, 32);
            this.textBoxNumberOfMsg.Name = "textBoxNumberOfMsg";
            this.textBoxNumberOfMsg.Size = new System.Drawing.Size(220, 20);
            this.textBoxNumberOfMsg.TabIndex = 2;
            this.textBoxNumberOfMsg.Text = "1";
            this.textBoxNumberOfMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumberOfMsg_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number Of Messages:";
            // 
            // buttonPublish
            // 
            this.buttonPublish.Location = new System.Drawing.Point(476, 4);
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Size = new System.Drawing.Size(75, 48);
            this.buttonPublish.TabIndex = 4;
            this.buttonPublish.Text = "Publish";
            this.buttonPublish.UseVisualStyleBackColor = true;
            this.buttonPublish.Click += new System.EventHandler(this.buttonPublish_Click);
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
            this.textBoxMessages.Size = new System.Drawing.Size(539, 191);
            this.textBoxMessages.TabIndex = 5;
            // 
            // buttonCreateConsumer
            // 
            this.buttonCreateConsumer.Location = new System.Drawing.Point(395, 6);
            this.buttonCreateConsumer.Name = "buttonCreateConsumer";
            this.buttonCreateConsumer.Size = new System.Drawing.Size(75, 46);
            this.buttonCreateConsumer.TabIndex = 6;
            this.buttonCreateConsumer.Text = "New Consumer";
            this.buttonCreateConsumer.UseVisualStyleBackColor = true;
            this.buttonCreateConsumer.Click += new System.EventHandler(this.buttonCreateConsumer_Click);
            // 
            // PublisherApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 261);
            this.Controls.Add(this.buttonCreateConsumer);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonPublish);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNumberOfMsg);
            this.Controls.Add(this.textBoxTopic);
            this.Name = "PublisherApp";
            this.Text = "PublisherApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PublisherApp_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.TextBox textBoxNumberOfMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPublish;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Button buttonCreateConsumer;
    }
}