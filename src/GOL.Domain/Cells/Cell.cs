namespace GOL.Domain.Cells
{
    public class Cell
    {
        // Initialize cell to start dead
        public Cell()
        {
            CellState = CellState.Dead;
        }

        // State of the cell
        public CellState CellState { get; set; }
    }
}