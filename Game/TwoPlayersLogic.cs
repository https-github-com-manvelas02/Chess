using ClassicChess.Entities;
using ClassicChess.Entities.Figurs;
using ClassicChess.Entities.Figurs.Combines;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;
using System.Linq;

namespace Game
{
    /// <summary>
    /// This class is responsible for working with the two players game process
    /// </summary>
    public class TwoPlayersLogic
    {
        public bool CanFigureMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions, out bool isMat)
        {
            IFigure figur = startCell.Figur;
            IFigure newFigur = null;
            isMat = false;
            if (this.CanMove(startCell, endCell, board, boardFunctions))
            {
                if (figur.GetType() == typeof(Knight))
                {
                    newFigur = new Knight(figur.Color);
                }
                else if (figur.GetType() == typeof(Pawn))
                {
                    newFigur = new Pawn(figur.Color);
                }
                else if (figur.GetType() == typeof(Bishop))
                {
                    newFigur = new Bishop(figur.Color);
                }
                else if (figur.GetType() == typeof(Rook))
                {
                    newFigur = new Rook(figur.Color);
                }
                else if (figur.GetType() == typeof(Queen))
                {
                    newFigur = new Queen(figur.Color);
                }
                else if (figur.GetType() == typeof(King))
                {
                    newFigur = new King(figur.Color);
                }
                newFigur.Number = endCell.Number;
                newFigur.Letter = endCell.Letter;
                newFigur.FigureHistory.AddRange(figur.FigureHistory);
                newFigur.FigureHistory.Add((startCell, endCell));
                board.History.Add((figur, (startCell, endCell)));
                board.Cells.FirstOrDefault(cell => cell == endCell).Figur = newFigur;
                board.Cells.FirstOrDefault(cell => cell == startCell).Figur = null;
                if (this.CheckYourKingShah(endCell.Figur.Color, board, boardFunctions))
                {
                    board.Cells.FirstOrDefault(cell => cell == endCell).Figur = null;
                    board.Cells.FirstOrDefault(cell => cell == startCell).Figur = newFigur;
                    return false;
                }
                else
                {
                    Cell kingCell;
                    if (endCell.Figur.Color == FigursColors.Green)
                    {
                        kingCell = this.GetOpponentKing(FigursColors.Red, board);
                    }
                    else
                    {
                        kingCell = this.GetOpponentKing(FigursColors.Green, board);
                    }
                    this.ChangeFigurBackgraund(kingCell, (ConsoleColor)kingCell.Color, board);
                }
                board.Cells.FirstOrDefault(cell => cell == endCell).Figur.colorBackgraund = (ConsoleColor)board.Cells.FirstOrDefault(cell => cell == endCell).Color;
                if (this.CheckKingShah(endCell, board, boardFunctions))
                {
                    isMat = this.IsMat(endCell, board, boardFunctions);
                    return true;
                }
                return true;
            }

            return false;
        }
        public bool IsPat(FigursColors figurColor, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> figures;
            if (figurColor == FigursColors.Red)
            {
                figures = this.GetOpponentFigures(FigursColors.Green, board);
            }
            else
            {
                figures = this.GetOpponentFigures(FigursColors.Red, board);
            }
            for (int i = 0; i < figures.Count; i++)
            {
                for (int j = 0; j < board.Cells.Count; j++)
                {
                    if (this.CanMove(figures[i], board.Cells[j], board, boardFunctions))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void ChangeFigure(Cell cell, IFigure figure, Board board, FunctionsForBoard boardFunctions)
        {
            board.Cells.FirstOrDefault(c => c == cell).Figur = figure;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Number = cell.Number;
            board.Cells.FirstOrDefault(c => c == cell).Figur.Letter = cell.Letter;
            board.Cells.FirstOrDefault(c => c == cell).Figur.colorBackgraund = (ConsoleColor)board.Cells.FirstOrDefault(c => c == cell).Color;
        }
        private bool CanMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {

            List<Cell> Road = new List<Cell>();

            IFigure figur = startCell.Figur;
            if (figur.IsMove(endCell))
            {
                if (figur.GetType() == typeof(Knight))
                {
                    return true;
                }
                else if (figur.GetType() == typeof(King))
                {
                    if (this.CanKingMove(startCell, endCell, board, boardFunctions))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                Road = this.GetRoad(startCell, endCell, board, boardFunctions);
                for (int i = 0; i < Road.Count; i++)
                {
                    if (Road[i].Figur != null)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        private List<Cell> GetRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> Road = new List<Cell>();

            IFigure figur = startCell.Figur;

            if (figur.GetType() == typeof(Pawn))
            {
                Road = this.PawnMoveRoad(startCell, endCell, board, boardFunctions);
            }
            else if (figur.GetType() == typeof(Bishop))
            {
                Road = this.BishopMoveRoad(startCell, endCell, board, boardFunctions);
            }
            else if (figur.GetType() == typeof(Rook))
            {
                Road = this.RookMoveRoad(startCell, endCell, board, boardFunctions);
            }
            else if (figur.GetType() == typeof(Queen))
            {
                Road = this.QueenMoveRoad(startCell, endCell, board, boardFunctions);
            }
            return Road;
        }
        private bool IsMat(Cell cell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> opponentFigures = this.GetOpponentFigures(cell.Figur.Color, board);
            Cell KingCell = this.GetOpponentKing(cell.Figur.Color, board);
            List<Cell> Road = this.GetRoad(cell, KingCell, board, boardFunctions);
            Road.Add(cell);
            for (int i = 0; i < opponentFigures.Count; i++)
            {
                for (int j = 0; j < Road.Count; j++)
                {
                    if (this.CanMove(opponentFigures[i], Road[j], board, boardFunctions))
                    {
                        IFigure figure = board.Cells.FirstOrDefault(c => c == opponentFigures[i]).Figur;
                        IFigure tempfigure = null;
                        bool haveRoadJFigure;
                        if (board.Cells.FirstOrDefault(c => c == Road[j]).Figur != null)
                        {
                            tempfigure = board.Cells.FirstOrDefault(c => c == Road[j]).Figur;
                        }
                        board.Cells.FirstOrDefault(c => c == Road[j]).Figur = figure;
                        board.Cells.FirstOrDefault(c => c == opponentFigures[i]).Figur = null;
                        if (!CheckYourKingShah(Road[j].Figur.Color, board, boardFunctions))
                        {
                            board.Cells.FirstOrDefault(c => c == opponentFigures[i]).Figur = figure;
                            board.Cells.FirstOrDefault(c => c == Road[j]).Figur = tempfigure;
                            return false;
                        }
                        board.Cells.FirstOrDefault(c => c == opponentFigures[i]).Figur = figure;
                        board.Cells.FirstOrDefault(c => c == Road[j]).Figur = tempfigure;
                    }
                }
            }
            this.ChangeFigurBackgraund(KingCell, ConsoleColor.Blue, board);
            return true;
        }
        private bool CheckKingShah(Cell cell, Board board, FunctionsForBoard boardFunctions)
        {
            Cell endCell = this.GetOpponentKing(cell.Figur.Color, board);
            if (this.CanMove(cell, endCell, board, boardFunctions))
            {
                this.ChangeFigurBackgraund(endCell, ConsoleColor.DarkYellow, board);
                return true;
            }
            else
            {
                this.ChangeFigurBackgraund(endCell, (ConsoleColor)endCell.Color, board);
                return false;
            }
        }
        private void ChangeFigurBackgraund(Cell cell, ConsoleColor color, Board board)
        {
            board.Cells.FirstOrDefault(c => c == cell).Figur.colorBackgraund = color;
        }
        private bool CheckYourKingShah(FigursColors figurColor, Board board, FunctionsForBoard boardFunctions)
        {
            Cell endCell;
            if (figurColor == FigursColors.Red)
            {
                endCell = this.GetOpponentKing(FigursColors.Green, board);
            }
            else
            {
                endCell = this.GetOpponentKing(FigursColors.Red, board);
            }
            return IsKingShah(endCell, board, endCell.Figur.Color, boardFunctions);
        }
        private List<Cell> GetOpponentFigures(FigursColors figursColors, Board board)
        {
            List<Cell> list = new List<Cell>();
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Figur != null)
                {
                    if (figursColors != board.Cells[i].Figur.Color)
                    {
                        list.Add(board.Cells[i]);
                    }
                }
            }
            return list;
        }
        private Cell GetOpponentKing(FigursColors figursColors, Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Figur != null)
                {
                    if (board.Cells[i].Figur.GetType() == typeof(King) && board.Cells[i].Figur.Color != figursColors)
                    {
                        return board.Cells[i];
                    }
                }
            }
            return null;
        }
        private List<Cell> PawnMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> list = new List<Cell>();
            if (Math.Abs(startCell.Number - endCell.Number) == 2)
            {
                int start;
                if (startCell.Figur.Color == FigursColors.Red)
                {
                    start = (int)endCell.Number + 1;
                }
                else
                {
                    start = (int)startCell.Number + 1;
                }
                string position = (char)startCell.Letter + start.ToString();
                Cell cell = boardFunctions.GetCellByPosition(position, board);
                list.Add(cell);
            }
            return list;
        }
        private List<Cell> BishopMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> bishopRoad = new List<Cell>();
            int startNum;
            int endNum;
            char startLet;
            if (startCell.Letter > endCell.Letter && startCell.Number > endCell.Number)
            {
                startLet = (char)(startCell.Letter - 1);
                startNum = (int)(startCell.Number - 1);
                endNum = (int)(endCell.Number + 1);
                if (startNum == endNum)
                {
                    string position = startLet + startNum.ToString();
                    Cell cell = boardFunctions.GetCellByPosition(position, board);
                    bishopRoad.Add(cell);
                }
                else
                {
                    for (int i = startNum; i <= endNum; i--)
                    {
                        string position = startLet + i.ToString();
                        Cell cell = boardFunctions.GetCellByPosition(position, board);
                        bishopRoad.Add(cell);
                        startLet--;
                    }
                }
            }
            else if (startCell.Letter > endCell.Letter && startCell.Number < endCell.Number)
            {
                startLet = (char)(startCell.Letter - 1);
                startNum = (int)(startCell.Number + 1);
                endNum = (int)(endCell.Number - 1);
                for (int i = startNum; i <= endNum; i++)
                {
                    string position = startLet + i.ToString();
                    Cell cell = boardFunctions.GetCellByPosition(position, board);
                    bishopRoad.Add(cell);
                    startLet--;
                }
            }
            else if (startCell.Letter < endCell.Letter && startCell.Number > endCell.Number)
            {
                startLet = (char)(startCell.Letter + 1);
                startNum = (int)(startCell.Number - 1);
                endNum = (int)(endCell.Number + 1);
                for (int i = startNum; i <= endNum; i--)
                {
                    string position = startLet + i.ToString();
                    Cell cell = boardFunctions.GetCellByPosition(position, board);
                    bishopRoad.Add(cell);
                    startLet++;
                }
            }
            else
            {
                startLet = (char)(startCell.Letter + 1);
                startNum = (int)(startCell.Number + 1);
                endNum = (int)(endCell.Number - 1);
                for (int i = startNum; i <= endNum; i++)
                {
                    string position = startLet + i.ToString();
                    Cell cell = boardFunctions.GetCellByPosition(position, board);
                    bishopRoad.Add(cell);
                    startLet++;
                }
            }
            return bishopRoad;
        }
        private List<Cell> RookMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> rookRoad = new List<Cell>();
            if (startCell.Number == endCell.Number)
            {
                char startLet;
                char endLet;
                int num = (int)startCell.Number;

                if (startCell.Letter > endCell.Letter)
                {
                    endLet = (char)(startCell.Letter - 1);
                    startLet = (char)endCell.Letter;
                    for (char i = endLet; i > startLet; i--)
                    {
                        string position = i + num.ToString();
                        Cell cell = boardFunctions.GetCellByPosition(position, board);
                        rookRoad.Add(cell);
                    }
                }
                else
                {
                    startLet = (char)(startCell.Letter + 1);
                    endLet = (char)endCell.Letter;
                    for (char i = startLet; i < endLet; i++)
                    {
                        string position = i + num.ToString();
                        Cell cell = boardFunctions.GetCellByPosition(position, board);
                        rookRoad.Add(cell);
                    }
                }
                return rookRoad;
            }
            else
            {
                int startNum;
                int endNum;
                char let = (char)startCell.Letter;
                if (startCell.Number > endCell.Number)
                {
                    startNum = (int)endCell.Number;
                    endNum = (int)startCell.Number;
                }
                else
                {
                    startNum = (int)startCell.Number;
                    endNum = (int)endCell.Number;
                }
                endNum--;
                startNum++;
                for (int i = startNum; i <= endNum; i++)
                {
                    string position = let + i.ToString();
                    Cell cell = boardFunctions.GetCellByPosition(position, board);
                    rookRoad.Add(cell);
                }
                return rookRoad;
            }
        }
        private List<Cell> QueenMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> queenRoad = new List<Cell>();
            if (startCell.Number == endCell.Number || startCell.Letter == endCell.Letter)
            {
                queenRoad = this.RookMoveRoad(startCell, endCell, board, boardFunctions);
            }
            else
            {
                queenRoad = this.BishopMoveRoad(startCell, endCell, board, boardFunctions);
            }
            return queenRoad;
        }
        private bool CanKingMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            FigursColors figursColors = startCell.Figur.Color;
            if (endCell.Figur == null)
            {
                if (!IsKingShah(endCell, board, figursColors, boardFunctions))
                {
                    return true;
                }
            }
            else
            {
                if (endCell.Figur.Color != figursColors)
                {
                    Cell tempCell = new Cell(endCell);
                    if (!IsKingShah(tempCell, board, figursColors, boardFunctions))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsKingShah(Cell cell, Board board, FigursColors figurColor, FunctionsForBoard boardFunctions)
        {
            List<Cell> cells = this.GetOpponentFigures(figurColor, board);
            for (int i = 0; i < cells.Count; i++)
            {
                if (this.CanMove(cells[i], cell, board, boardFunctions))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
