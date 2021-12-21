using ClassicChess.Entities.Figurs.Combines;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;

namespace ClassicChess.Entities
{
    /// <summary>
    /// This class is about a chess cell
    /// </summary>
    public class Cell
    {
        public Cell(Cell cell)
        {
            this.Color =  cell.Color;
            this.Letter = cell.Letter;
            this.Number = cell.Number;
            this.Figur = null;
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
