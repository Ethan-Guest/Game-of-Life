﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        private readonly Color cellColor = Color.Gray;

        // Drawing colors
        private readonly Color gridColor = Color.Black;

        // The Timer class
        private readonly Timer timer = new Timer();

        // The universe array
        private readonly bool[,] universe = new bool[30, 30];

        // Generation count
        private int generations;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running
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
                if (universe[x, y]) e.Graphics.FillRectangle(cellBrush, cellRect);

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
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
    }
}