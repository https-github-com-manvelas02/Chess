using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Classes.Figurs.Interface;

namespace Game
{
    public class TaskMethods
    {
        public Board board = new Board();
        public List<IFigure> Figurs = new List<IFigure>();
        FunctionsForBoard FunctionsForBoard = new FunctionsForBoard();
        TasksForKnight TasksForKnight = new TasksForKnight();
        TwoPlayersLogic TwoPlayersLogic = new TwoPlayersLogic();

        #region Task For Board
        public bool IsTruePosition(string position)
        {
            return FunctionsForBoard.IsTruePosition(position);
        }
        public (int, char) GetPosition(string position)
        {
            return FunctionsForBoard.GetPosition(position);
        }
        public Cell GetCellByPosition(int num, char letter)
        {
            return FunctionsForBoard.GetCellByPosition(num, letter, this.board);
        }
        public void CreateFigires()
        {
            Figurs = FunctionsForBoard.CreateFigires();
        }
        public void AddFiguresOnBoard()
        {
            this.board = FunctionsForBoard.AddFiguresOnBoard(this.Figurs, this.board);
        }
        public void AddKnightToBoard(Cell cell)
        {
            Knight knight = (Knight)this.Figurs[0];
            FunctionsForBoard.AddKnightToBoard(cell, this.board, knight);
        }
        #endregion

        #region Knight Tasks
        public void CreateOnlyKnight()
        {
            Knight knight = TasksForKnight.CreateOnlyKnight();
            Figurs.Add(knight);
        }
        public List<Cell> GetKnightRoad(Cell startCell, Cell endCell)
        {
            return TasksForKnight.GetKnightRoad(startCell, endCell, this.board);
        }
        #endregion

        #region Two Player Logic
        public bool CanFigureMove(Cell startCell, Cell endCell)
        {
            return TwoPlayersLogic.CanFigureMove(startCell, endCell, this.board, FunctionsForBoard);
        }
        public bool CheckKingShah(Cell cell)
        {
            return TwoPlayersLogic.CheckKingShah(cell, this.board, FunctionsForBoard);
        }
        #endregion
    }
}