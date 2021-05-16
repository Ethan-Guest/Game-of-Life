using System.ComponentModel;

namespace GOL
{
    partial class RandomSeed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.OkButton = new System.Windows.Forms.Button();
            this.CanelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(12, 63);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(97, 27);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CanelButton
            // 
            this.CanelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CanelButton.Location = new System.Drawing.Point(124, 63);
            this.CanelButton.Name = "CanelButton";
            this.CanelButton.Size = new System.Drawing.Size(97, 27);
            this.CanelButton.TabIndex = 1;
            this.CanelButton.Text = "Cancel\r\n";
            this.CanelButton.UseVisualStyleBackColor = true;
            // 
            // RandomSeed
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CanelButton;
            this.ClientSize = new System.Drawing.Size(233, 102);
            this.Controls.Add(this.CanelButton);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RandomSeed";
            this.Text = "RandomSeed";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button OkButton;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        #endregion
    }
}