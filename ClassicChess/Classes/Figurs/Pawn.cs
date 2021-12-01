using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes.Figurs
{
    public class Pawn : IFigure
    {
        public Pawn(FigursColors color)
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
                if (this.Color == FigursColors.Green)
                {
                    if ((int)(this.Number) == 2)
                    {
                        if (this.Letter == cell.Letter && cell.Number - this.Number <= 2)
                        {
                            if (cell.Figur == null)
                            {
                                return true;
                            }
                        }
                    }
                    else if (Math.Abs(this.Letter - cell.Letter) == 1 && cell.Number - this.Number == 1)
                    {
                        if (cell.Figur != null)
                        {
                            if (cell.Figur.Color != this.Color)
                            {
                                return true;
                            }
                        }
                    }
                    else if (this.Letter == cell.Letter && cell.Number - this.Number == 1)
                    {
                        if (cell.Figur == null)
                        {
                            return true;
                        }
                    }
                }
                else if (this.Color == FigursColors.Red)
                {
                    if ((int)(this.Number) == 7)
                    {
                        if (this.Letter == cell.Letter && this.Number - cell.Number <= 2)
                        {
                            if (cell.Figur == null)
                            {
                                return true;
                            }
                        }
                    }
                    else if (Math.Abs(this.Letter - cell.Letter) == 1 && this.Number - cell.Number == 1)
                    {
                        if (cell.Figur != null)
                        {
                            if (cell.Figur.Color != this.Color)
                            {
                                return true;
                            }
                        }
                    }
                    else if (this.Letter == cell.Letter && this.Number - cell.Number == 1)
                    {
                        if (cell.Figur == null)
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
