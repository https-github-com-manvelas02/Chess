using ClassicChess.Entities.Figurs.Combines;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;

namespace ClassicChess.Entities.Figurs
{
    /// <summary>
    /// This class is about the chess piece of the King
    /// </summary>
    public class King : IFigure
    {
        public Numbers Number { get; set; }
        public Letters Letter { get; set; }
        public FigursColors Color { get; }
        public CellsColors colorBackgraund { get; set; }
        public List<(Cell, Cell)> FigureHistory { get; set; } = new List<(Cell, Cell)>();

        public King(FigursColors color)
        {
            this.Color = color;
        }


        public bool IsMove(Cell cell)
        {
            if (!IsSamePos(cell))
            {
                if (Math.Abs(this.Number - cell.Number) <= 1 && Math.Abs(this.Letter - cell.Letter) <= 1)
                {
                    if (cell.Figur == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (cell.Figur.Color != this.Color)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsSamePos(Cell cell)
        {
            if (this.Number == cell.Number && this.Letter == cell.Letter)
            {
                return true;
            }
            return false;
        }

    }
}
