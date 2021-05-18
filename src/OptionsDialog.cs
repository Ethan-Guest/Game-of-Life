using System;
using System.Windows.Forms;

namespace GOL
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        public int Interval { get; set; }
        public int UniverseWidth { get; set; }
        public int UniverseHeight { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Interval = int.Parse(TimerIntervalNumeric.Text);
            UniverseWidth = int.Parse(WidthBox.Text);
            UniverseHeight = int.Parse(HeightBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}