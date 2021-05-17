using System;
using System.Windows.Forms;

namespace GOL
{
    public partial class RandomSeed : Form
    {
        public RandomSeed()
        {
            InitializeComponent();
        }

        public int SeedValue { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            SeedValue = int.Parse(SeedValueInput.Text);
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