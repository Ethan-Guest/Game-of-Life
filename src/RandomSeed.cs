using System;
using System.Windows.Forms;

namespace GOL
{
    public partial class RandomSeed : Form
    {
        /// <summary>
        ///     The random seed class
        /// </summary>
        public RandomSeed()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Seed value held in int
        /// </summary>
        public int SeedValue { get; set; }


        /// <summary>
        ///     Randomize seed ok
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            SeedValue = int.Parse(SeedValueInput.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        ///     Randomize seed cancel
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///     Randomize button
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            RandomSeedGenerator();
        }

        /// <summary>
        ///     Generate a random seed when the form is loaded
        /// </summary>
        private void RandomSeed_Load(object sender, EventArgs e)
        {
            RandomSeedGenerator();
        }

        /// <summary>
        ///     Randomize seed within range of int (-2147483648 to 2147483647)
        /// </summary>
        private void RandomSeedGenerator()
        {
            var r = new Random();
            SeedValueInput.Text = r.Next(-2147483648, 2147483647).ToString();
        }
    }
}