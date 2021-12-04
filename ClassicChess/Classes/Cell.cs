﻿using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes
{
    /// <summary>
    /// This class is about a chess cell
    /// </summary>
    public class Cell
    {
        public Cell(Cell cell)
        {
            cell.Color = this.Color;
            cell.Letter = this.Letter;
            cell.Number = this.Number;
            cell.Figur = null;
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
