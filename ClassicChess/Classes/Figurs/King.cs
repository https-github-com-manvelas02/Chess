using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes.Figurs
{
    public class King : IFigure
    {
        public King(FigursColors color)
        {
            this.Color = color;
        }


        public Numbers Number { get; set; }
        public Letters Letter { get; set; }
        public FigursColors Color { get; }

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
                        else
                        {
                            return false;
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
