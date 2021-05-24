namespace GOL.Domain.Cells
{
    public class Cell
    {
        /// <summary>
        ///     Initialize cell to start dead
        /// </summary>
        public Cell()
        {
            CellState = CellState.Dead;
        }

        /// <summary>
        ///     State of the cell
        /// </summary>
        public CellState CellState { get; set; }
    }
}