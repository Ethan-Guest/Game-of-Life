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

        private void OkButton_Click(object sender, EventArgs e)
        {
            return DialogResult.OK(1);
        }
    }
}