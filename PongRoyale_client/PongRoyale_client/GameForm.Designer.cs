using PongRoyale_client.Game;

namespace PongRoyale_client
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.FrameCountLabel = new System.Windows.Forms.Label();
            this.ConnectToServerButton = new System.Windows.Forms.Button();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.RichTextBox();
            this.SyncLoop = new System.Windows.Forms.Timer(this.components);
            this.StartGameButton = new System.Windows.Forms.Button();
            this.StartLocalButton = new System.Windows.Forms.Button();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.GameScreen = new PongRoyale_client.Game.GameScreen();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Interval = 10;
            // 
            // FrameCountLabel
            // 
            this.FrameCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameCountLabel.Location = new System.Drawing.Point(642, 9);
            this.FrameCountLabel.Name = "FrameCountLabel";
            this.FrameCountLabel.Size = new System.Drawing.Size(160, 15);
            this.FrameCountLabel.TabIndex = 0;
            this.FrameCountLabel.Text = "FrameCount";
            this.FrameCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConnectToServerButton
            // 
            this.ConnectToServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectToServerButton.Location = new System.Drawing.Point(642, 58);
            this.ConnectToServerButton.Name = "ConnectToServerButton";
            this.ConnectToServerButton.Size = new System.Drawing.Size(160, 23);
            this.ConnectToServerButton.TabIndex = 1;
            this.ConnectToServerButton.Text = "Connect to Server";
            this.ConnectToServerButton.UseVisualStyleBackColor = true;
            this.ConnectToServerButton.Click += new System.EventHandler(this.ConnectToServerButton_Click);
            // 
            // ChatInput
            // 
            this.ChatInput.Location = new System.Drawing.Point(0, 432);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(190, 20);
            this.ChatInput.TabIndex = 6;
            this.ChatInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChatInput_Submitted);
            // 
            // Chat
            // 
            this.Chat.Location = new System.Drawing.Point(0, 1);
            this.Chat.Name = "Chat";
            this.Chat.ReadOnly = true;
            this.Chat.Size = new System.Drawing.Size(190, 431);
            this.Chat.TabIndex = 5;
            this.Chat.Text = "";
            // 
            // StartGameButton
            // 
            this.StartGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartGameButton.Location = new System.Drawing.Point(642, 87);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(160, 23);
            this.StartGameButton.TabIndex = 8;
            this.StartGameButton.Text = "Start game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // StartLocalButton
            // 
            this.StartLocalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartLocalButton.Location = new System.Drawing.Point(642, 116);
            this.StartLocalButton.Name = "StartLocalButton";
            this.StartLocalButton.Size = new System.Drawing.Size(160, 23);
            this.StartLocalButton.TabIndex = 9;
            this.StartLocalButton.Text = "local game";
            this.StartLocalButton.UseVisualStyleBackColor = true;
            this.StartLocalButton.Click += new System.EventHandler(this.StartLocalButton_Click);
            // 
            // IPTextBox
            // 
            this.IPTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IPTextBox.Location = new System.Drawing.Point(642, 32);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(159, 20);
            this.IPTextBox.TabIndex = 10;
            this.IPTextBox.Text = "78.58.170.115:6969";
            this.IPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GameScreen
            // 
            this.GameScreen.BackColor = System.Drawing.SystemColors.Control;
            this.GameScreen.Location = new System.Drawing.Point(183, 1);
            this.GameScreen.Name = "GameScreen";
            this.GameScreen.Size = new System.Drawing.Size(451, 451);
            this.GameScreen.TabIndex = 7;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 450);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.StartLocalButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.GameScreen);
            this.Controls.Add(this.ChatInput);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.ConnectToServerButton);
            this.Controls.Add(this.FrameCountLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "Pong Royale";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.Label FrameCountLabel;
        private System.Windows.Forms.Button ConnectToServerButton;
        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.RichTextBox Chat;
        private PongRoyale_client.Game.GameScreen GameScreen;
        private System.Windows.Forms.Timer SyncLoop;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Button StartLocalButton;
        private System.Windows.Forms.TextBox IPTextBox;
    }
}

