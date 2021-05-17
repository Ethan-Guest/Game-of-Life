using System;
using System.Drawing;
using System.Windows.Forms;
using GOL.Domain.Cells;

namespace GOL
{
    public partial class Form1 : Form
    {
        private readonly Color cellColor = Color.LimeGreen;

        // Drawing colors
        private readonly Color gridColor = Color.FromArgb(29, 29, 29);

        // The Timer class
        private readonly Timer timer = new Timer();

        // Generation count
        private int generations;

        // The ScratchPad array
        private Cell[,] scratchPad = new Cell[64, 36];

        // The universe array
        private Cell[,] universe = new Cell[64, 36];

        public Form1()
        {
            for (var index0 = 0; index0 < universe.GetLength(0); index0++)
            for (var index1 = 0; index1 < universe.GetLength(1); index1++)
                universe[index0, index1] = new Cell();
            for (var index0 = 0; index0 < scratchPad.GetLength(0); index0++)
            for (var index1 = 0; index1 < scratchPad.GetLength(1); index1++)
                scratchPad[index0, index1] = new Cell();
            InitializeComponent();
            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running
        }

        // Count neighbor methods
        private int CountNeighborsFinite(int x, int y)
        {
            var liveNeighbors = 0;
            var xLen = universe.GetLength(0);
            var yLen = universe.GetLength(1);
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            for (var yOffset = -1; yOffset <= 1; yOffset++)
            {
                var xCheck = x + xOffset;
                var yCheck = y + yOffset;
                if (xOffset == 0 && yOffset == 0) continue;
                if (xCheck < 0) continue;
                if (yCheck < 0) continue;
                if (xCheck >= xLen) continue;
                if (yCheck >= yLen) continue;
                if (universe[xCheck, yCheck].CellState == CellState.Alive) liveNeighbors++;
            }

            return liveNeighbors;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            var liveNeighbors = 0;
            var xLen = universe.GetLength(0);
            var yLen = universe.GetLength(1);
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            for (var yOffset = -1; yOffset <= 1; yOffset++)
            {
                var xCheck = x + xOffset;
                var yCheck = y + yOffset;
                if (xOffset == 0 && yOffset == 0) continue;
                if (xCheck < 0) xCheck = xLen - 1;
                if (yCheck < 0) yCheck = yLen - 1;
                if (xCheck >= xLen) xCheck = 0;
                if (yCheck >= yLen) yCheck = 0;
                if (universe[xCheck, yCheck].CellState == CellState.Alive) liveNeighbors++;
            }

            return liveNeighbors;
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            for (var x = 0; x < universe.GetLength(0); x++)
            for (var y = 0; y < universe.GetLength(1); y++)
            {
                scratchPad[x, y].CellState = universe[x, y].CellState;
                var liveNeighbors = tToolStripMenuItem.Checked
                    ? CountNeighborsToroidal(x, y)
                    : CountNeighborsFinite(x, y);
                switch (universe[x, y].CellState)
                {
                    case CellState.Alive when liveNeighbors < 2:
                    case CellState.Alive when liveNeighbors > 3:
                        scratchPad[x, y].CellState = CellState.Dead;
                        break;
                    case CellState.Alive when liveNeighbors == 2 || liveNeighbors == 3:
                    case CellState.Dead when liveNeighbors == 3:
                        scratchPad[x, y].CellState = CellState.Alive;
                        break;
                }
            }

            var temp = universe;
            universe = scratchPad;
            scratchPad = temp;
            generations++;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations;
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            var cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            var cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            var gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (var y = 0; y < universe.GetLength(1); y++)
                // Iterate through the universe in the x, left to right
            for (var x = 0; x < universe.GetLength(0); x++)
            {
                // A rectangle to represent each cell in pixels
                var cellRect = Rectangle.Empty;
                cellRect.X = x * cellWidth;
                cellRect.Y = y * cellHeight;
                cellRect.Width = cellWidth;
                cellRect.Height = cellHeight;
                // Fill the cell with a brush if alive
                if (universe[x, y].CellState == CellState.Alive)
                {
                    // Add neighborCount numbers
                    e.Graphics.FillRectangle(cellBrush, cellRect);
                    var font = new Font("Arial", 8f);
                    var stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    var rect = new Rectangle(0, 0, 100, 100);
                    var cell = universe[x, y];
                    var liveNeighbors = tToolStripMenuItem.Checked
                        ? CountNeighborsToroidal(x, y)
                        : CountNeighborsFinite(x, y);
                    e.Graphics.DrawString(liveNeighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                }

                e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                var cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                var cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                var x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                var y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y].CellState =
                    universe[x, y].CellState == CellState.Alive ? CellState.Dead : CellState.Alive;
                scratchPad[x, y].CellState =
                    scratchPad[x, y].CellState == CellState.Alive ? CellState.Dead : CellState.Alive;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Play
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            graphicsPanel1.Invalidate();
        }

        // Pause
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            graphicsPanel1.Invalidate();
        }

        // Next
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }

        // New
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            universe = new Cell[64, 36];
            for (var index0 = 0; index0 < universe.GetLength(0); index0++)
            for (var index1 = 0; index1 < universe.GetLength(1); index1++)
                universe[index0, index1] = new Cell();
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations;
            graphicsPanel1.Invalidate();
        }


        // Toroidal
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fToolStripMenuItem.Checked = false;
            tToolStripMenuItem.Checked = true;
            graphicsPanel1.Invalidate();
        }

        // Finite
        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tToolStripMenuItem.Checked = false;
            fToolStripMenuItem.Checked = true;
            graphicsPanel1.Invalidate();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new RandomSeed();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                var s = new Random(dlg.SeedValue);
                for (var x = 0; x < universe.GetLength(0); x++)
                for (var y = 0; y < universe.GetLength(1); y++)
                {
                    var r = s.Next(0, 2);
                    universe[x, y].CellState = r == 0 ? CellState.Alive : CellState.Dead;
                }
            }


            graphicsPanel1.Invalidate();
        }
    }
}