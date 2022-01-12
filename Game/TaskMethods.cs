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
        private Knight knight;
        private TwoPlayersLogic twoPlayersLogic = new TwoPlayersLogic();
        

        #region Task For Board
        public Cell GetCellByPosition(string position)
        {
            Cell cell = this.board.GetCellByPosition(position);
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
        public void CreateFigires()
        {
            Figurs = this.board.CreateFigires();
        }
        public void AddFiguresOnBoard()
        {
            this.board.AddFiguresOnBoard(this.Figurs);
        }
        public void AddKnightToBoard(string position)
        {
            Cell cell = this.board.GetCellByPosition(position);
            this.board.AddKnightToBoard(cell, knight);
        }
        public bool CheckCellFigure()
        {
            return this.board.ChekCellFigure(startCell);
        }
        #endregion

        #region Knight Tasks
        public void CreateOnlyKnight()
        {
            knight = new Knight(FigursColors.White);
        }
        public List<string> GetKnightRoad(string startPosition, string endPosition)
        {
            this.GetCellByPosition(startPosition);
            this.GetCellByPosition(endPosition);
            return knight.GetKnightRoad(startCell, endCell, this.board);
        }
        #endregion

        #region Two Player Logic
        
        /// <summary>
        /// This function determines whether the figure can be moved
        /// </summary>
        /// <param name="startCell">The figure start cell</param>
        /// <param name="endCell">The target cell</param>
        /// <returns>True if the figure can move.Otherwise false</returns>
        public bool CanFigureMove()
        {
            bool isFigureMove = twoPlayersLogic.CanMove(startCell, endCell,this.board);
            return isFigureMove;
        }
        public bool IsPat()
        {
            return this.twoPlayersLogic.IsPat(endCell.Figur.Color, this.board);
        }
        public void ChangePawn(string figureName)
        {
            IFigure figure = this.GetFigureType(figureName, endCell.Figur.Color);
            this.twoPlayersLogic.ChangeFigure(endCell, figure, this.board);
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
        public bool IsMat()
        {
            return twoPlayersLogic.IsMat(endCell, board);
        }
        #endregion

        #region Another Tasks
        
        public string GetHistory()
        {
            string history = "";
            for (int i = 0; i < this.board.History.Count; i++)
            {
                history += $"{this.board.History[i].Item1.Color} {this.board.History[i].Item1.GetType().Name} " +
                    $" {this.board.History[i].Item2.Item1.Letter}{(int)this.board.History[i].Item2.Item1.Number}"+
                    $" - {this.board.History[i].Item2.Item2.Letter}{(int)this.board.History[i].Item2.Item2.Number}\n";
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
            if (this.startCell.Figur.Color == FigursColors.White)
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