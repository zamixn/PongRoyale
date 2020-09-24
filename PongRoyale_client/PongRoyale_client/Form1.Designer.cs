namespace PongRoyale_client
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
            this.components = new System.ComponentModel.Container();
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.FrameCountLabel = new System.Windows.Forms.Label();
            this.ConnectToServerButton = new System.Windows.Forms.Button();
            this.ServerResponseLabel = new System.Windows.Forms.Label();
            this.SendDataToServer = new System.Windows.Forms.Button();
            this.DataToServerTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Interval = 10;
            this.GameLoop.Tick += new System.EventHandler(this.GameLoop_Tick);
            // 
            // FrameCountLabel
            // 
            this.FrameCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameCountLabel.Location = new System.Drawing.Point(628, 9);
            this.FrameCountLabel.Name = "FrameCountLabel";
            this.FrameCountLabel.Size = new System.Drawing.Size(160, 15);
            this.FrameCountLabel.TabIndex = 0;
            this.FrameCountLabel.Text = "FrameCount";
            this.FrameCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConnectToServerButton
            // 
            this.ConnectToServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectToServerButton.Location = new System.Drawing.Point(628, 27);
            this.ConnectToServerButton.Name = "ConnectToServerButton";
            this.ConnectToServerButton.Size = new System.Drawing.Size(160, 23);
            this.ConnectToServerButton.TabIndex = 1;
            this.ConnectToServerButton.Text = "Connect to Server";
            this.ConnectToServerButton.UseVisualStyleBackColor = true;
            this.ConnectToServerButton.Click += new System.EventHandler(this.ConnectToServerButton_Click);
            // 
            // ServerResponseLabel
            // 
            this.ServerResponseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerResponseLabel.Location = new System.Drawing.Point(625, 108);
            this.ServerResponseLabel.Name = "ServerResponseLabel";
            this.ServerResponseLabel.Size = new System.Drawing.Size(163, 324);
            this.ServerResponseLabel.TabIndex = 2;
            this.ServerResponseLabel.Text = "ServerInfo:\r\nasdfsadfasgdfhfsghfg";
            this.ServerResponseLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SendDataToServer
            // 
            this.SendDataToServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SendDataToServer.Location = new System.Drawing.Point(628, 82);
            this.SendDataToServer.Name = "SendDataToServer";
            this.SendDataToServer.Size = new System.Drawing.Size(160, 23);
            this.SendDataToServer.TabIndex = 3;
            this.SendDataToServer.Text = "Send";
            this.SendDataToServer.UseVisualStyleBackColor = true;
            this.SendDataToServer.Click += new System.EventHandler(this.SendDataToServer_Click);
            // 
            // DataToServerTextBox
            // 
            this.DataToServerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataToServerTextBox.Location = new System.Drawing.Point(628, 56);
            this.DataToServerTextBox.Name = "DataToServerTextBox";
            this.DataToServerTextBox.Size = new System.Drawing.Size(160, 20);
            this.DataToServerTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DataToServerTextBox);
            this.Controls.Add(this.SendDataToServer);
            this.Controls.Add(this.ServerResponseLabel);
            this.Controls.Add(this.ConnectToServerButton);
            this.Controls.Add(this.FrameCountLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.Label FrameCountLabel;
        private System.Windows.Forms.Button ConnectToServerButton;
        private System.Windows.Forms.Label ServerResponseLabel;
        private System.Windows.Forms.Button SendDataToServer;
        private System.Windows.Forms.TextBox DataToServerTextBox;
    }
}

