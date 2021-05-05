using GOL.Domain.Dimensions;
using GOL.Domain.Positions;

namespace GOL.Domain.Cells
{
    public class Cell
    {
        public CellState CellState { get; set; }
        public Dimension Dimension { get; set; }
        public Position Position { get; set; }
        public int Generations { get; set; }
    }
}