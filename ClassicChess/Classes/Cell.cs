using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes
{
    public class Cell
    {
        public Cell(Cell cell)
        {
            this.Number = cell.Number;
            this.Color =  cell.Color;
            this.Figur =  cell.Figur;
            this.Letter = cell.Letter;
        }
        public Cell(Numbers number, Letters letter, CellsColors color)
        {
            this.Color = color;
            this.Number = number;
            this.Letter = letter;
        }
        public IFigure Figur { get; set; }
        public Numbers Number { get; private set; }
        public Letters Letter { get; private set; }
        public CellsColors Color { get; private set; }
    }
}
