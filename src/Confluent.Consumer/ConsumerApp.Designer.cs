namespace Confluent.Consumer
{
    partial class ConsumerApp
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
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConsumerGroup = new System.Windows.Forms.TextBox();
            this.textBoxConsumerId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOffSet = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(104, 6);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(220, 20);
            this.textBoxTopic.TabIndex = 0;
            this.textBoxTopic.Text = "test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Topic:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Consumer Group:";
            // 
            // textBoxConsumerGroup
            // 
            this.textBoxConsumerGroup.Location = new System.Drawing.Point(104, 32);
            this.textBoxConsumerGroup.Name = "textBoxConsumerGroup";
            this.textBoxConsumerGroup.Size = new System.Drawing.Size(220, 20);
            this.textBoxConsumerGroup.TabIndex = 0;
            this.textBoxConsumerGroup.Text = "testgroup";
            // 
            // textBoxConsumerId
            // 
            this.textBoxConsumerId.Location = new System.Drawing.Point(104, 58);
            this.textBoxConsumerId.Name = "textBoxConsumerId";
            this.textBoxConsumerId.Size = new System.Drawing.Size(220, 20);
            this.textBoxConsumerId.TabIndex = 0;
            this.textBoxConsumerId.Text = "testconsumer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Consumer Id:";
            // 
            // buttonOffSet
            // 
            this.buttonOffSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOffSet.Location = new System.Drawing.Point(567, 6);
            this.buttonOffSet.Name = "buttonOffSet";
            this.buttonOffSet.Size = new System.Drawing.Size(109, 72);
            this.buttonOffSet.TabIndex = 3;
            this.buttonOffSet.Text = "Start";
            this.buttonOffSet.UseVisualStyleBackColor = true;
            this.buttonOffSet.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessages.Location = new System.Drawing.Point(12, 84);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessages.Size = new System.Drawing.Size(663, 382);
            this.textBoxMessages.TabIndex = 4;
            // 
            // labelCount
            // 
            this.labelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCount.Location = new System.Drawing.Point(331, 6);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(202, 72);
            this.labelCount.TabIndex = 5;
            this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConsumerApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 478);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonOffSet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxConsumerId);
            this.Controls.Add(this.textBoxConsumerGroup);
            this.Controls.Add(this.textBoxTopic);
            this.Name = "ConsumerApp";
            this.Text = "Consumer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxConsumerGroup;
        private System.Windows.Forms.TextBox textBoxConsumerId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOffSet;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Label labelCount;
    }
}

