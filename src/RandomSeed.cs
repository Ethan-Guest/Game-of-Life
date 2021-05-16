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

        private void button1_Click(object sender, EventArgs e)
        {
            button1.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.DialogResult = DialogResult.Cancel;
        }
    }
}