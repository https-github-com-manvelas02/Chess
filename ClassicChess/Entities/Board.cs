using ClassicChess.Entities.Figurs.Combines;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;

namespace ClassicChess.Entities
{
    /// <summary>
    /// This class is about a chess board
    /// </summary>
    public class Board
    {
        public Board()
        {
            this.CreateBoard();
        }
        public int Width { get; } = 16;
        public List<int> Numbers { get; set; } = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<char> Letters { get; set; } = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        public List<Cell> Cells { get; private set; } = new List<Cell>(64);
        public List<(IFigure, (Cell, Cell))> History { get; set; } = new List<(IFigure, (Cell, Cell))>();

        /// <summary>
        /// This function creates a standard chessboard
        /// </summary>
        private void CreateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                int letter = 65;
                for (int j = 0; j < 8; j++)
                {
                    Cell cell;
                    if ((i + j) % 2 == 0)
                    {
                        cell = new Cell((Numbers)Numbers[i], (Letters)letter, CellsColors.Black);
                    }
                    else
                    {
                        cell = new Cell((Numbers)Numbers[i], (Letters)letter, CellsColors.White);
                    }
                    Cells.Add(cell);
                    letter++;
                }

            }
        }

        /// <summary>
        /// This function checks whether the position is on the chessboard or not
        /// </summary>
        /// <param name="position">This is the position we want to know</param>
        /// <returns>This function will return true if there is a cell in that position and that cell is on the chessboard. Otherwise false</returns>
        public bool IsTruePosition(string position)
        {
            if (position.Length == 2)
            {
                int digite;
                bool isDigite = int.TryParse(position[1].ToString(), out digite);
                if (isDigite)
                {
                    char letter = position[0].ToString().ToUpper()[0];
                    if (digite > 0 && digite <= 8 && letter >= 'A' && letter <= 'H')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckAreFigureInCell(Cell cell)
        {
            if (cell.Figur != null)
            {
                return true;
            }
            return false;
        }
    }
}
