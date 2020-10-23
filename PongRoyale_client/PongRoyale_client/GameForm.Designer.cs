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
            this.SyncLoop = new System.Windows.Forms.Timer(this.components);
            this.MainMenu = new PongRoyale_client.Menu.MainMenu();
            this.GameScreen = new PongRoyale_client.Game.GameplayScreen();
            this.ChatUI = new PongRoyale_client.Menu.ChatUI();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Interval = 10;
            // 
            // FrameCountLabel
            // 
            this.FrameCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameCountLabel.Location = new System.Drawing.Point(677, 9);
            this.FrameCountLabel.Name = "FrameCountLabel";
            this.FrameCountLabel.Size = new System.Drawing.Size(160, 15);
            this.FrameCountLabel.TabIndex = 0;
            this.FrameCountLabel.Text = "FrameCount";
            this.FrameCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(675, 27);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(162, 411);
            this.MainMenu.TabIndex = 11;
            // 
            // GameScreen
            // 
            this.GameScreen.BackColor = System.Drawing.SystemColors.Control;
            this.GameScreen.Location = new System.Drawing.Point(202, -1);
            this.GameScreen.Name = "GameScreen";
            this.GameScreen.Size = new System.Drawing.Size(451, 451);
            this.GameScreen.TabIndex = 7;
            // 
            // ChatUI
            // 
            this.ChatUI.Location = new System.Drawing.Point(0, 1);
            this.ChatUI.Name = "ChatUI";
            this.ChatUI.Size = new System.Drawing.Size(196, 449);
            this.ChatUI.TabIndex = 12;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 450);
            this.Controls.Add(this.ChatUI);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.GameScreen);
            this.Controls.Add(this.FrameCountLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "Pong Royale";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.Label FrameCountLabel;
        private PongRoyale_client.Game.GameplayScreen GameScreen;
        private System.Windows.Forms.Timer SyncLoop;
        private Menu.MainMenu MainMenu;
        private Menu.ChatUI ChatUI;
    }
}

