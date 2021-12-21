using ClassicChess.Entities;
using ClassicChess.Entities.Figurs;
using ClassicChess.Entities.Figurs.Combines;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;

namespace Game
{
    public class TaskMethods
    {
        public Board board = new Board();
        public List<IFigure> Figurs = new List<IFigure>();
        private Cell startCell;
        private Cell endCell;
        FunctionsForBoard FunctionsForBoard = new FunctionsForBoard();
        TasksForKnight TasksForKnight = new TasksForKnight();
        TwoPlayersLogic TwoPlayersLogic = new TwoPlayersLogic();

        #region Task For Board
        /// <summary>
        /// This function gives the cell coordinates
        /// </summary>
        /// <param name="position">The position of the cell in which we will find the coordinates</param>
        /// <returns>Cell by position</returns>
        public Cell GetCellByPosition(string position)
        {
            Cell cell = FunctionsForBoard.GetCellByPosition(position, this.board);
            if (startCell == null)
            {
                startCell = cell;
            }
            else
            {
                endCell = cell;
            }
            return cell;
        }
        /// <summary>
        /// Function for creating chess figures
        /// </summary>
        /// <returns>The created Figure list</returns>
        public void CreateFigires()
        {
            Figurs = FunctionsForBoard.CreateFigires();
        }
        /// <summary>
        /// This function add the created figures in board
        /// </summary>
        /// <returns>The already finished board lined up with the figures</returns>
        public void AddFiguresOnBoard()
        {
            this.board = FunctionsForBoard.AddFiguresOnBoard(this.Figurs, this.board);
        }
        /// <summary>
        /// This function add Knight in the board
        /// </summary>
        /// <param name="cell">The cell you want to add</param>
        public void AddKnightToBoard(string position)
        {
            Cell cell = FunctionsForBoard.GetCellByPosition(position, this.board);
            Knight knight = (Knight)this.Figurs[0];
            FunctionsForBoard.AddKnightToBoard(cell, this.board, knight);
        }
        public bool CheckCellFigure()
        {
            return FunctionsForBoard.ChekCellFigure(startCell, this.board);
        }
        #endregion

        #region Knight Tasks
        /// <summary>
        /// This function create a knight
        /// </summary>
        /// <returns>The created Knight</returns>
        public void CreateOnlyKnight()
        {
            Knight knight = TasksForKnight.CreateOnlyKnight();
            Figurs.Add(knight);
        }
        /// <summary>
        /// This function get the Knight shortest Road
        /// </summary>
        /// <param name="startCell">The Knight Cell</param>
        /// <param name="endCell">The target Cell</param>
        /// <returns>The horse shortest Road List by steps </returns>
        public List<string> GetKnightRoad(string startPosition, string endPosition)
        {
            this.GetCellByPosition(startPosition);
            this.GetCellByPosition(endPosition);
            return TasksForKnight.GetKnightRoad(startCell, endCell, this.board);
        }
        #endregion

        #region Two Player Logic
        /// <summary>
        /// This function determines whether the figure can be moved
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <returns>True if the figure can move.Otherwise false</returns>
        public bool CanFigureMove(out bool isMat)
        {
            bool isFigureMove = TwoPlayersLogic.CanFigureMove(startCell, endCell, this.board, FunctionsForBoard, out isMat);
            return isFigureMove;
        }
        public bool IsPat()
        {
            return this.TwoPlayersLogic.IsPat(endCell.Figur.Color,this.board,this.FunctionsForBoard);
        }
        public void ChangePawn(string figureName)
        {
            IFigure figure = this.GetFigureType(figureName, endCell.Figur.Color);
            this.TwoPlayersLogic.ChangeFigure(endCell, figure, this.board, this.FunctionsForBoard);
        }
        public bool IsChangePawn()
        {
            if (endCell.Number == Numbers.one || endCell.Number == Numbers.eight)
            {
                if (endCell.Figur.GetType() == typeof(Pawn))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Another Tasks

        public string GetHistory()
        {
            string history = "";
            for (int i = 0; i < this.board.History.Count; i++)
            {
                history += $"{this.board.History[i].Item1.Color} {this.board.History[i].Item1.GetType().Name} " +
                    $" {this.board.History[i].Item2.Item1.Letter}{(int)this.board.History[i].Item2.Item1.Number} - {this.board.History[i].Item2.Item2.Letter}{(int)this.board.History[i].Item2.Item2.Number}\n";
            }
            return history;
        }
        public void ClearPositions()
        {
            this.startCell = null;
            this.endCell = null;
        }
        public bool GiveFigureColor()
        {
            if (this.startCell.Figur.Color == FigursColors.Green)
            {
                return true;
            }
            return false;
        }
        
        private IFigure GetFigureType(string figureName,FigursColors figursColors)
        {
            if (figureName == typeof(King).Name)
            {
                return new King(figursColors);
            }
            else if (figureName == typeof(Knight).Name)
            {
                return new Knight(figursColors);
            }
            else if(figureName == typeof(Pawn).Name)
            {
                return new Pawn(figursColors);
            }
            else if(figureName == typeof(Bishop).Name)
            {
                return new Bishop(figursColors);
            }
            else if(figureName == typeof(Rook).Name)
            {
                return new Rook(figursColors);
            }
            else if(figureName == typeof(Queen).Name)
            {
                return new Queen(figursColors);
            }
            return null;
        }
        #endregion
    }
}