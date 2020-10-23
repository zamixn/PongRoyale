namespace PongRoyale_client.Menu
{
    partial class MainMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.StartLocalButton = new System.Windows.Forms.Button();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.ConnectToServerButton = new System.Windows.Forms.Button();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IPTextBox.Location = new System.Drawing.Point(13, 47);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(159, 20);
            this.IPTextBox.TabIndex = 14;
            this.IPTextBox.Text = "78.58.170.115:6969";
            this.IPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartLocalButton
            // 
            this.StartLocalButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartLocalButton.Location = new System.Drawing.Point(13, 131);
            this.StartLocalButton.Name = "StartLocalButton";
            this.StartLocalButton.Size = new System.Drawing.Size(160, 23);
            this.StartLocalButton.TabIndex = 13;
            this.StartLocalButton.Text = "local game";
            this.StartLocalButton.UseVisualStyleBackColor = true;
            this.StartLocalButton.Click += new System.EventHandler(this.StartLocalButton_Click);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartGameButton.Location = new System.Drawing.Point(13, 102);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(160, 23);
            this.StartGameButton.TabIndex = 12;
            this.StartGameButton.Text = "Start game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // ConnectToServerButton
            // 
            this.ConnectToServerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConnectToServerButton.Location = new System.Drawing.Point(13, 73);
            this.ConnectToServerButton.Name = "ConnectToServerButton";
            this.ConnectToServerButton.Size = new System.Drawing.Size(160, 23);
            this.ConnectToServerButton.TabIndex = 11;
            this.ConnectToServerButton.Text = "Connect to Server";
            this.ConnectToServerButton.UseVisualStyleBackColor = true;
            this.ConnectToServerButton.Click += new System.EventHandler(this.ConnectToServerButton_Click);
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.Location = new System.Drawing.Point(13, 179);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(56, 17);
            this.DebugCheckBox.TabIndex = 15;
            this.DebugCheckBox.Text = "debug";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            this.DebugCheckBox.CheckedChanged += new System.EventHandler(this.DebugCheckBox_CheckedChanged);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.StartLocalButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.ConnectToServerButton);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(186, 205);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox IPTextBox;
        public System.Windows.Forms.Button StartLocalButton;
        public System.Windows.Forms.Button StartGameButton;
        public System.Windows.Forms.Button ConnectToServerButton;
        private System.Windows.Forms.CheckBox DebugCheckBox;
    }
}
