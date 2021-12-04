using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace Game
{
    /// <summary>
    /// This class is responsible for working with the chessboard
    /// </summary>
    public class FunctionsForBoard
    {
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
                int digitePosition = 0;
                int tempDigite;
                bool isDigite = int.TryParse(position[digitePosition].ToString(), out digite);
                bool isLetter = !int.TryParse(position[1 - digitePosition].ToString(), out tempDigite);
                if ((isDigite && isLetter))
                {
                    char letter = position[1 - digitePosition].ToString().ToUpper()[0];
                    if (digite > 0 && digite <= 8 && letter >= 'A' && letter <= 'H')
                    {
                        return true;
                    }
                }
                else if (!isDigite && !isLetter)
                {
                    digitePosition = 1;
                    digite = tempDigite;
                    char letter = position[1 - digitePosition].ToString().ToUpper()[0];
                    if (digite > 0 && digite <= 8 && letter >= 'A' && letter <= 'H')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// This function gives the cell coordinates
        /// </summary>
        /// <param name="position">The position of the cell in which we will find the coordinates</param>
        /// <returns>Int32 as a cell number and Char as a cell letter</returns>
        public (int, char) GetPosition(string position)
        {
            int digitePosition = 0;
            int digite;
            char letter;
            bool isDigite = int.TryParse(position[digitePosition].ToString(), out digite);
            if (!isDigite)
            {
                digitePosition = 1;
                isDigite = int.TryParse(position[digitePosition].ToString(), out digite);
            }
            //we take the letter index of the position (the index of the number minus 1), which creates a string, then that string.ToUpper (),
            //which will capitalize all the letters in our string and take the first element of the resulting string
            letter = ((position[1 - digitePosition].ToString()).ToUpper())[0];

            return (digite, letter);
        }
        
        /// <summary>
        /// This function finds the cell from the board according to the cell coordinates
        /// </summary>
        /// <param name="num">The cell number</param>
        /// <param name="letter">The cell Letter</param>
        /// <param name="board">The board from which we want to find the cell</param>
        /// <returns>The cell he found</returns>
        public Cell GetCellByPosition(int num, char letter, Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Number == (Numbers)num && board.Cells[i].Letter == (Letters)letter)
                {
                    return board.Cells[i];
                }
            }
            return null;
        }
        
        /// <summary>
        /// Function for creating chess figures
        /// </summary>
        /// <returns>The created Figure list</returns>
        public List<IFigure> CreateFigires()
        {
            List<IFigure> Figurs = new List<IFigure>();
            FigursColors figursColors;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    figursColors = FigursColors.Green;
                }
                else
                {
                    figursColors = FigursColors.Red;
                }

                Rook rook1 = new Rook(figursColors);
                Knight knigth1 = new Knight(figursColors);
                Bishop bishop1 = new Bishop(figursColors);
                Queen queen = new Queen(figursColors);
                King king = new King(figursColors);
                Rook rook2 = new Rook(figursColors);
                Knight knigth2 = new Knight(figursColors);
                Bishop bishop2 = new Bishop(figursColors);
                Figurs.Add(rook1);
                Figurs.Add(knigth1);
                Figurs.Add(bishop1);
                Figurs.Add(queen);
                Figurs.Add(king);
                Figurs.Add(bishop2);
                Figurs.Add(knigth2);
                Figurs.Add(rook2);
                for (int j = 0; j < 8; j++)
                {
                    Pawn pawn = new Pawn(figursColors);
                    Figurs.Add(pawn);
                }
            }
            return Figurs;
        }
        
        /// <summary>
        /// This function add the created figures in board
        /// </summary>
        /// <param name="Figurs">The figures List</param>
        /// <param name="board">The board on which we want to line up the figures</param>
        /// <returns>The already finished board lined up with the figures</returns>
        public Board AddFiguresOnBoard(List<IFigure> Figurs, Board board)
        {
            if (CanAddFiguresOnBoard(board))
            {
                int cellIndex = 0;
                for (int i = 0; i < Figurs.Count; i++)
                {
                    if (i == Figurs.Count / 2)
                    {
                        cellIndex = 56;
                    }
                    else if (i == Figurs.Count - 8)
                    {
                        cellIndex = 48;
                    }
                    board.Cells[cellIndex].Figur = Figurs[i];
                    board.Cells[cellIndex].Figur.Number = board.Cells[cellIndex].Number;
                    board.Cells[cellIndex].Figur.Letter = board.Cells[cellIndex].Letter;
                    cellIndex++;
                }
            }
            return board;
        }
        
        /// <summary>
        /// This function add Knight in the board
        /// </summary>
        /// <param name="cell">The cell you want to add</param>
        /// <param name="board">The board you want to add</param>
        /// <param name="knight">Knight to be added to the given cell on the given board</param>
        public void AddKnightToBoard(Cell cell, Board board, Knight knight)
        {
            board.Cells.FirstOrDefault(c => c == cell).Figur = knight;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Number = cell.Number;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Letter = cell.Letter;
        }

        /// <summary>
        /// This function checks if we can add figures to the board
        /// </summary>
        /// <param name="board">The board we want to check</param>
        /// <returns>True if there is no figure on the board. Otherwise false</returns>
        private bool CanAddFiguresOnBoard(Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Figur != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
