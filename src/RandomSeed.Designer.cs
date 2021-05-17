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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SeedValueInput = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.SeedValueInput)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(152, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SeedValueInput
            // 
            this.SeedValueInput.Location = new System.Drawing.Point(48, 24);
            this.SeedValueInput.Maximum = new decimal(new int[] {2147483647, 0, 0, 0});
            this.SeedValueInput.Minimum = new decimal(new int[] {-2147483648, 0, 0, -2147483648});
            this.SeedValueInput.Name = "SeedValueInput";
            this.SeedValueInput.Size = new System.Drawing.Size(120, 20);
            this.SeedValueInput.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(175, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Randomize";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // RandomSeed
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(306, 97);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.SeedValueInput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RandomSeed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RandomSeed";
            ((System.ComponentModel.ISupportInitialize) (this.SeedValueInput)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.NumericUpDown SeedValueInput;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button3;
    }
}