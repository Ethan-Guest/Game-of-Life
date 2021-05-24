using System;
using System.Windows.Forms;

namespace GOL
{
    /// <summary>
    ///     Options dialog class
    /// </summary>
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        public int Interval { get; set; }
        public int UniverseWidth { get; set; }
        public int UniverseHeight { get; set; }

        /// <summary>
        ///     Options ok
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Interval = int.Parse(TimerIntervalNumeric.Text);
            UniverseWidth = int.Parse(WidthBox.Text);
            UniverseHeight = int.Parse(HeightBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        ///     Options cancel
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}