using System.ComponentModel;

namespace GOL
{
    partial class OptionsDialog
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
            this.TimerIntervalNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WidthBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.HeightBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize) (this.TimerIntervalNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.WidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.HeightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TimerIntervalNumeric
            // 
            this.TimerIntervalNumeric.Location = new System.Drawing.Point(173, 43);
            this.TimerIntervalNumeric.Maximum = new decimal(new int[] {10000, 0, 0, 0});
            this.TimerIntervalNumeric.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.TimerIntervalNumeric.Name = "TimerIntervalNumeric";
            this.TimerIntervalNumeric.Size = new System.Drawing.Size(120, 20);
            this.TimerIntervalNumeric.TabIndex = 2;
            this.TimerIntervalNumeric.Value = new decimal(new int[] {100, 0, 0, 0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timer Interval In Milliseconds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Universe Width";
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(173, 69);
            this.WidthBox.Minimum = new decimal(new int[] {3, 0, 0, 0});
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(120, 20);
            this.WidthBox.TabIndex = 4;
            this.WidthBox.Value = new decimal(new int[] {64, 0, 0, 0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Universe Height";
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(173, 95);
            this.HeightBox.Minimum = new decimal(new int[] {3, 0, 0, 0});
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(120, 20);
            this.HeightBox.TabIndex = 6;
            this.HeightBox.Value = new decimal(new int[] {36, 0, 0, 0});
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 210);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.HeightBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WidthBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimerIntervalNumeric);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.Text = "OptionsDialog";
            ((System.ComponentModel.ISupportInitialize) (this.TimerIntervalNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.WidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.HeightBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.NumericUpDown WidthBox;
        private System.Windows.Forms.NumericUpDown HeightBox;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        #endregion

        private System.Windows.Forms.NumericUpDown TimerIntervalNumeric;
        private System.Windows.Forms.Label label1;
    }
}