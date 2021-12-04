using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes
{
    /// <summary>
    /// This class is about a chess board
    /// </summary>
    public class Board
    {
        public Board()
        {
            if (this.Cells.Count == 0)
            {
                this.CreateBoard();
            }
        }
        public int Width { get; } = 16;
        public List<int> Numbers { get; set; } = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<char> Letters { get; set; } = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        public List<Cell> Cells { get; private set; } = new List<Cell>(64);

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

    }
}
