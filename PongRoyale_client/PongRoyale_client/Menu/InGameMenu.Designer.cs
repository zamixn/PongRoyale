namespace PongRoyale_client.Menu
{
    partial class InGameMenu
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
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.QuitGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.Location = new System.Drawing.Point(8, 38);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(56, 17);
            this.DebugCheckBox.TabIndex = 17;
            this.DebugCheckBox.Text = "debug";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            this.DebugCheckBox.CheckedChanged += new System.EventHandler(this.DebugCheckBox_CheckedChanged);
            // 
            // QuitGameButton
            // 
            this.QuitGameButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.QuitGameButton.Location = new System.Drawing.Point(8, 9);
            this.QuitGameButton.Name = "QuitGameButton";
            this.QuitGameButton.Size = new System.Drawing.Size(160, 23);
            this.QuitGameButton.TabIndex = 16;
            this.QuitGameButton.Text = "Quit";
            this.QuitGameButton.UseVisualStyleBackColor = true;
            this.QuitGameButton.Click += new System.EventHandler(this.QuitGameButton_Click);
            // 
            // InGameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.QuitGameButton);
            this.Name = "InGameMenu";
            this.Size = new System.Drawing.Size(177, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DebugCheckBox;
        public System.Windows.Forms.Button QuitGameButton;
    }
}
