﻿using System;
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
        private Cell[,] scratchPad = new Cell[30, 30];

        // The universe array
        private Cell[,] universe = new Cell[30, 30];

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
            var count = 0;
            var xLen = universe.GetLength(0);
            var yLen = universe.GetLength(1);

            for (var xOffset = -1; xOffset < +1; xOffset++)
            for (var yOffset = -1; yOffset <= 1; yOffset++)
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

        private int CountNeighborsToroidal(int x, int y)
        {
            var count = 0;
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
                if (universe[xCheck, yCheck].CellState == CellState.Alive) count++;
            }

            return count;
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            //Iterate through the universe in the x, left to right
            for (var x = 0; x < universe.GetLength(0); x++)
            for (var y = 0; y < universe.GetLength(1); y++)
            {
                var count = CountNeighborsToroidal(x, y);

                // Apply rules
                // Should cell live or die
                // Turn on/off in scratchPad


                if (universe[x, y].CellState == CellState.Alive && count < 2)
                    scratchPad[x, y].CellState = CellState.Dead;
                else if (universe[x, y].CellState == CellState.Alive && count > 3)
                    scratchPad[x, y].CellState = CellState.Dead;
                else if (universe[x, y].CellState == CellState.Dead && count == 3)
                    scratchPad[x, y].CellState = CellState.Alive;
                else
                    scratchPad[x, y].CellState = universe[x, y].CellState;
                /*{
                    // Any living cell in the current universe with less than 2 living neighbors dies in the next generation as if by under-population. 
                    // Any living cell with more than 3 living neighbors will die in the next generation as if by over-population.
                    // Any living cell with 2 or 3 living neighbors will live on into the next generation. 
                    if (count == 2 || count == 3) scratchPad[x, y].CellState = CellState.Alive;
                }
                // Any dead cell with exactly 3 living neighbors will be born into the next generation as if by reproduction. 
                if (scratchPad[x, y].CellState == CellState.Dead)
                    if (count == 3)
                        scratchPad[x, y].CellState = CellState.Alive;*/
            }

            // Copy the scratchPad into the universe
            var temp = universe;
            universe = scratchPad;
            scratchPad = temp;
            // Increment generation count
            generations++;

            // Update status strip generations
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
                    e.Graphics.FillRectangle(cellBrush, cellRect);

                    var font = new Font("Arial", 20f);

                    var stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    var rect = new Rectangle(0, 0, 100, 100);
                    var cell = universe[x, y];
                    var neighbors = CountNeighborsToroidal(x, y);

                    e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Red, cellRect, stringFormat);
                }

                e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);


                // Outline the cell with a pen
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }
    }
}