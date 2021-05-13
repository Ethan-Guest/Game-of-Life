namespace GOL.Domain.Cells
{
    public class Cell
    {
        public Cell()
        {
            CellState = CellState.Dead;
        }

        public CellState CellState { get; set; }
        // public Dimension Dimension { get; set; }
        // public Position Position { get; set; }
        //public int Generations { get; set; }
    }
}