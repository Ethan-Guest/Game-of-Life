using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GOL.Domain.Cells;
using GOL.Properties;

namespace GOL
{
    public partial class Form1 : Form
    {
        // The Timer class
        private readonly Timer timer = new Timer();

        // Live cell count
        private int aliveCells;

        // Color of the cell
        private Color cellColor = Color.LimeGreen;

        // Generation count
        private int generations;

        // Color of the grid
        private Color gridColor = Color.FromArgb(29, 29, 29);

        // Heads up display
        private bool isHUDVisible = true;

        // The ScratchPad array
        private Cell[,] scratchPad;

        // The universe array
        private Cell[,] universe;


        // Initialize
        public Form1()
        {
            InitializeUniverse(Settings.Default.UniverseWidth, Settings.Default.UniverseHeight);
            InitializeComponent();
            // Setup the timer
            timer.Interval = Settings.Default.Interval; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running
            graphicsPanel1.BackColor = Settings.Default.BackColor;
            cellColor = Settings.Default.CellColor;
            gridColor = Settings.Default.GridColor;
        }

        /// <summary>
        ///     Initialize a new universe
        /// </summary>
        /// <param name="width">Width of the universe</param>
        /// <param name="height">Height of the universe</param>
        private void InitializeUniverse(int width, int height)
        {
            universe = new Cell[width, height];
            for (var index0 = 0; index0 < universe.GetLength(0); index0++)
            for (var index1 = 0; index1 < universe.GetLength(1); index1++)
                universe[index0, index1] = new Cell();

            scratchPad = new Cell[width, height];
            for (var index0 = 0; index0 < scratchPad.GetLength(0); index0++)
            for (var index1 = 0; index1 < scratchPad.GetLength(1); index1++)
                scratchPad[index0, index1] = new Cell();
        }

        /// <summary>
        ///     Finite method
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Toroidal method
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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
            aliveCells = 0;
            for (var x = 0; x < universe.GetLength(0); x++)
            for (var y = 0; y < universe.GetLength(1); y++)
            {
                if (universe[x, y].CellState == CellState.Alive) aliveCells++;
                scratchPad[x, y].CellState = universe[x, y].CellState;
                var liveNeighbors = tToolStripMenuItem.Checked
                    ? CountNeighborsToroidal(x, y)
                    : CountNeighborsFinite(x, y);

                // Rules
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
            toolStripStatusLabelGenerations.Text = $@"Generations = {generations}";
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        /// <summary>
        ///     Graphics panel 1
        /// </summary>
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
                    // Add neighbor count numbers
                    e.Graphics.FillRectangle(cellBrush, cellRect);
                    var font = new Font("Arial", 8f);
                    var stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    var liveNeighbors = tToolStripMenuItem.Checked
                        ? CountNeighborsToroidal(x, y)
                        : CountNeighborsFinite(x, y);

                    // Toggle Neighbor count
                    if (showNeighborCountToolStripMenuItem.Checked)
                        e.Graphics.DrawString(liveNeighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                }

                // Toggle grid
                if (gridToolStripMenuItem.Checked)
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                // Update alive cell count
                AliveCells.Text = $@"Alive = {aliveCells}";
            }

            // Heads up display
            if (isHUDVisible)
            {
                var type = fToolStripMenuItem.Checked ? "Finite" : "Toroidal";
                var font = new Font("Arial", 15f);
                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Near;
                var rect = new Rectangle(0, 0, 100, 100);
                e.Graphics.DrawString(
                    $@"Generations: {generations}{Environment.NewLine}Cell Count: {aliveCells}{Environment.NewLine}Boundary Type: {type}{Environment.NewLine}Universe Size: (Width={universe.GetLength(0)}, Height={universe.GetLength(1)})",
                    font, Brushes.WhiteSmoke, ClientRectangle, stringFormat);
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        /// <summary>
        ///     Graphics panel 1 mouse click event
        /// </summary>
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

                // Update alive cell count
                aliveCells++;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        /// <summary>
        ///     Exit game option
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Start game tool strip button
        /// </summary>
        private void playToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Pause game tool strip button
        /// </summary>
        private void pauseToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Next tool strip button
        /// </summary>
        private void nextToolStripButton_Click(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Create new universe tool strip button
        /// </summary>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            InitializeUniverse(Settings.Default.UniverseWidth, Settings.Default.UniverseHeight);
            generations = 0;

            // Reset footer text
            toolStripStatusLabelGenerations.Text = $"Generations = {generations}";
            AliveCells.Text = $"Alive = {0}";

            graphicsPanel1.Invalidate();
        }


        /// <summary>
        ///     Toggle toroidal neighbor count method
        /// </summary>
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tool strip option
            fToolStripMenuItem.Checked = false;
            tToolStripMenuItem.Checked = true;

            // Context menu option
            finiteToolStripMenuItem.Checked = false;
            toroidalToolStripMenuItem.Checked = true;

            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Toggle finite neighbor count method
        /// </summary>
        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tool strip option
            tToolStripMenuItem.Checked = false;
            fToolStripMenuItem.Checked = true;

            // Context menu option
            toroidalToolStripMenuItem.Checked = false;
            finiteToolStripMenuItem.Checked = true;

            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Randomize from seed
        /// </summary>
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

        /// <summary>
        ///     Randomize from time
        /// </summary>
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = new Random();

            for (var x = 0; x < universe.GetLength(0); x++)
            for (var y = 0; y < universe.GetLength(1); y++)
            {
                var r = s.Next(0, 2);
                universe[x, y].CellState = r == 0 ? CellState.Alive : CellState.Dead;
            }

            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Toggle neighbor count text
        /// </summary>
        private void showNeighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNeighborCountToolStripMenuItem.Checked = !showNeighborCountToolStripMenuItem.Checked;
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Options dialog
        /// </summary>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OptionsDialog();
            if (DialogResult.OK != dlg.ShowDialog())
                return;
            var interval = dlg.Interval;
            var universeWidth = dlg.UniverseWidth;
            var universeHeight = dlg.UniverseHeight;
            timer.Interval = interval;
            Settings.Default.UniverseWidth = universeWidth;
            Settings.Default.UniverseHeight = universeHeight;
            Settings.Default.Interval = interval;
            InitializeUniverse(universeWidth, universeHeight);
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Toggle grid
        /// </summary>
        private void gridToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            graphicsPanel1.Invalidate();
        }

        /// <summary>
        ///     Cell color dialog
        /// </summary>
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = cellColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
                Settings.Default.CellColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        /// <summary>
        ///     Grid color dialog
        /// </summary>
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = gridColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
                Settings.Default.GridColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        /// <summary>
        ///     Background color dialog
        /// </summary>
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = graphicsPanel1.BackColor;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
                Settings.Default.BackColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        /// <summary>
        ///     Reset settings to default
        /// </summary>
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            graphicsPanel1.BackColor = Settings.Default.BackColor;
            cellColor = Settings.Default.CellColor;
            gridColor = Settings.Default.GridColor;
            InitializeUniverse(Settings.Default.UniverseWidth, Settings.Default.UniverseHeight);
            timer.Interval = Settings.Default.Interval;
        }

        /// <summary>
        ///     Reload settings to last saved settings
        /// </summary>
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            graphicsPanel1.BackColor = Settings.Default.BackColor;
            cellColor = Settings.Default.CellColor;
            gridColor = Settings.Default.GridColor;
            InitializeUniverse(Settings.Default.UniverseWidth, Settings.Default.UniverseHeight);
            timer.Interval = Settings.Default.Interval;
        }

        /// <summary>
        ///     Save settings when the forms box is closed
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            dlg.DefaultExt = "cells";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                var writer = new StreamWriter(dlg.FileName);
                writer.WriteLine("!This cell file was saved from Ethan Guest's Game of Life project.");

                // Iterate through the universe one row at a time.
                for (var y = 0; y < universe.GetLength(1); y++)
                {
                    var currentRow = string.Empty;
                    for (var x = 0; x < universe.GetLength(0); x++)
                        if (universe[x, y].CellState == CellState.Alive)
                            currentRow += "O";
                        else
                            currentRow += ".";
                    writer.WriteLine(currentRow);
                }

                writer.Close();
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                var reader = new StreamReader(dlg.FileName);
                var maxWidth = 0;
                var maxHeight = 0;
                var yPos = 0;

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    if (row[0] == '!') continue;
                    if (row[0] != '!') maxHeight++;
                    maxWidth = row.Length;
                }

                InitializeUniverse(maxWidth, maxHeight);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    if (row[0] == '!') continue;
                    for (var xPos = 0; xPos < row.Length; xPos++)
                        universe[xPos, yPos].CellState = row[xPos] == 'O' ? CellState.Alive : CellState.Dead;
                    yPos++;
                }

                reader.Close();
                graphicsPanel1.Invalidate();
            }
        }

        // Sync hud option with tool strip and context menu
        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hUDToolStripMenuItem.Checked = !hUDToolStripMenuItem.Checked;
            isHUDVisible = hUDToolStripMenuItem.Checked;
            hUDToolStripMenuItem1.Checked = !hUDToolStripMenuItem1.Checked;
            graphicsPanel1.Invalidate();
        }
    }
}