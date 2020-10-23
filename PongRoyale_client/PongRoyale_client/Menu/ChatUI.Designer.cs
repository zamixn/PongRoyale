namespace PongRoyale_client.Menu
{
    partial class ChatUI
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
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ChatInput
            // 
            this.ChatInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChatInput.Location = new System.Drawing.Point(3, 425);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(190, 20);
            this.ChatInput.TabIndex = 8;
            this.ChatInput.KeyDown += this.ChatInput_Submitted;
            // 
            // Chat
            // 
            this.Chat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Chat.Location = new System.Drawing.Point(3, 3);
            this.Chat.Name = "Chat";
            this.Chat.ReadOnly = true;
            this.Chat.Size = new System.Drawing.Size(190, 422);
            this.Chat.TabIndex = 7;
            this.Chat.Text = "";
            // 
            // ChatUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChatInput);
            this.Controls.Add(this.Chat);
            this.Name = "ChatUI";
            this.Size = new System.Drawing.Size(196, 449);
            this.Load += new System.EventHandler(this.ChatUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.RichTextBox Chat;
    }
}
