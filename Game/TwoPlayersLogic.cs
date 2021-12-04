using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums.Colors;

namespace Game
{
    /// <summary>
    /// This class is responsible for working with the two players game process
    /// </summary>
    public class TwoPlayersLogic
    {
        /// <summary>
        /// This function determines whether the figure can be moved
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>True if the figure can move.Otherwise false</returns>
        public bool CanFigureMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            IFigure figur = startCell.Figur;
            IFigure newFigur = null;
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
                board.Cells.FirstOrDefault(cell => cell == endCell).Figur = newFigur;
                board.Cells.FirstOrDefault(cell => cell == startCell).Figur = null;
                return true;
            }

            return false;
        }
        /// <summary>
        /// This function checks whether the figure has given a shah to the opponent's king
        /// </summary>
        /// <param name="cell">The figure cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>True if the figure shah to the opponent's king.Otherwise false</returns>
        public bool CheckKingShah(Cell cell, Board board, FunctionsForBoard boardFunctions)
        {
            Cell endCell = this.GetOpponentKing(cell.Figur.Color, board);
            if (this.CanMove(cell, endCell, board, boardFunctions))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// This function give opponent King
        /// </summary>
        /// <param name="figursColors">The color we want to find the king of the opposite color</param>
        /// <param name="board">The board we are playing on</param>
        /// <returns>The opponent King Cell</returns>
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
        /// <summary>
        /// This function determines can practically move the figure
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>True if the figure can move.Otherwise false</returns>
        private bool CanMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {

            List<Cell> Road = new List<Cell>();

            IFigure figur = startCell.Figur;
            IFigure newFigur = null;
            if (figur.IsMove(endCell))
            {
                if (figur.GetType() == typeof(Knight))
                {
                    return true;
                }
                else if (figur.GetType() == typeof(Pawn))
                {
                    Road = this.PawnMoveRoad(startCell, endCell, board, boardFunctions);
                    newFigur = new Pawn(figur.Color);
                }
                else if (figur.GetType() == typeof(Bishop))
                {
                    Road = this.BishopMoveRoad(startCell, endCell, board, boardFunctions);
                    newFigur = new Bishop(figur.Color);
                }
                else if (figur.GetType() == typeof(Rook))
                {
                    Road = this.RookMoveRoad(startCell, endCell, board, boardFunctions);
                    newFigur = new Rook(figur.Color);
                }
                else if (figur.GetType() == typeof(Queen))
                {
                    Road = this.QueenMoveRoad(startCell, endCell, board, boardFunctions);
                    newFigur = new Queen(figur.Color);
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
        /// <summary>
        /// The function give Pawn move Road
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>The Pawn move Road List by steps </returns>
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
                Cell cell = boardFunctions.GetCellByPosition(start, (char)startCell.Letter, board);
                list.Add(cell);
            }
            return list;
        }
        /// <summary>
        /// The function give Bishop move Road
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>The Bishop move Road List by steps </returns>
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
                    Cell cell = boardFunctions.GetCellByPosition(startNum, startLet, board);
                    bishopRoad.Add(cell);
                }
                else
                {
                    for (int i = startNum; i <= endNum; i--)
                    {
                        Cell cell = boardFunctions.GetCellByPosition(i, startLet, board);
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
                    Cell cell = boardFunctions.GetCellByPosition(i, startLet, board);
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
                    Cell cell = boardFunctions.GetCellByPosition(i, startLet, board);
                    bishopRoad.Add(cell);
                    startLet++;
                }
            }
            else
            {
                startLet = (char)(startCell.Letter + 1);
                startNum = (int)(startCell.Number + 1);
                endNum = (int)(endCell.Number - 1);
                for (int i = startNum; i >= endNum; i--)
                {
                    Cell cell = boardFunctions.GetCellByPosition(i, startLet, board);
                    bishopRoad.Add(cell);
                    startLet++;
                }
            }
            return bishopRoad;
        }
        /// <summary>
        /// The function give Rook move Road
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>The Rook move Road List by steps </returns>
        private List<Cell> RookMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> rookRoad = new List<Cell>();
            if (startCell.Number == endCell.Number)
            {
                char startLet;
                char endLet;
                if (startCell.Letter > endCell.Letter)
                {
                    endLet = (char)(startCell.Letter - 1);
                    startLet = (char)endCell.Letter;
                }
                else
                {
                    startLet = (char)(startCell.Letter + 1);
                    endLet = (char)endCell.Letter;
                }
                int num = (int)startCell.Number;
                for (char i = startLet; i < endLet; i++)
                {
                    Cell cell = boardFunctions.GetCellByPosition(num, i, board);
                    rookRoad.Add(cell);
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
                    Cell cell = boardFunctions.GetCellByPosition(i, let, board);
                    rookRoad.Add(cell);
                }
                return rookRoad;
            }
        }
        /// <summary>
        /// The function give Queen move Road
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>The Queen move Road List by steps </returns>
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
        /// <summary>
        /// The function checks whether a king can move to the target position or not
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>True if king can move to the target position.Otherwise False</returns>
        private bool CanKingMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<IFigure> figures = new List<IFigure>();
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
        /// <summary>
        /// The function checks is there shah or not
        /// </summary>
        /// <param name="cell">Cell for the possible shah</param>
        /// <param name="board">The board we are playing on</param>
        /// <param name="figurColor">The current color for the selected king figure</param>
        /// <param name="boardFunctions">The instance for  use board functions</param>
        /// <returns>True if the selected king's next move will be under shah.Otherwise false </returns>
        private bool IsKingShah(Cell cell, Board board, FigursColors figurColor, FunctionsForBoard boardFunctions)
        {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Figur != null)
                {
                    if (board.Cells[i].Figur.Color != figurColor)
                    {
                        cells.Add(board.Cells[i]);
                    }
                }
            }
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
