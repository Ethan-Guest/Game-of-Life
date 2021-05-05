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
        private readonly Color gridColor = Color.Black;

        // The ScratchPad array
        private readonly Cell[,] scratchPad = new Cell[30, 30];

        // The Timer class
        private readonly Timer timer = new Timer();

        // The universe array
        private readonly Cell[,] universe = new Cell[30, 30];


        // Generation count
        private int generations;

        public Form1()
        {
            for (var index0 = 0; index0 < universe.GetLength(0); index0++)
            for (var index1 = 0; index1 < universe.GetLength(1); index1++)
                universe[index0, index1] = new Cell();

            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running
        }

        private int CountNeighborsFinite(int x, int y)
        {
            var count = 0;
            var xLen = universe.GetLength(0);
            var yLen = universe.GetLength(1);

            for (var yOffset = -1; yOffset <= 1; yOffset++)
            for (var xOffset = -1; xOffset < +1; xOffset++)
            {
                var xCheck = x + xOffset;
                var yCheck = y + yOffset;

                if (xOffset == 0 && yOffset == 0) continue;

                if (xCheck < 0) continue;

                if (yCheck < 0) continue;

                if (xCheck >= xLen) continue;

                if (yCheck >= yLen) continue;

                if (universe[xCheck, yCheck].CellState == CellState.Alive) count++;
            }

            return count;
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations;
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
                if (universe[x, y].CellState == CellState.Alive) e.Graphics.FillRectangle(cellBrush, cellRect);

                // Outline the cell with a pen
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

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
    }
}