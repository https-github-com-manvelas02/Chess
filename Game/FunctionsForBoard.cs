using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace Game
{
    public class FunctionsForBoard
    {
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
        public (int, char) GetPosition(string position)
        {
            int digitePosition = 0;
            int digite = 0;
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
        public Board AddFiguresOnBoard(List<IFigure> Figurs, Board board)
        {
            if (CanAddFiguresOnBoard(board))
            {
                int j = 0;
                for (int i = 0; i < Figurs.Count; i++)
                {
                    if (i == Figurs.Count / 2)
                    {
                        j = 56;
                    }
                    else if (i == Figurs.Count - 8)
                    {
                        j = 48;
                    }
                    board.Cells[j].Figur = Figurs[i];
                    board.Cells[j].Figur.Number = board.Cells[j].Number;
                    board.Cells[j].Figur.Letter = board.Cells[j].Letter;
                    j++;
                }
            }
            return board;
        }
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
        public void AddKnightToBoard(Cell cell, Board board, Knight knight)
        {
            board.Cells.FirstOrDefault(c => c == cell).Figur = knight;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Number = cell.Number;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Letter = cell.Letter;
        }
    }
}
