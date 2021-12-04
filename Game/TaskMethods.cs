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
        /// <summary>
        /// This function checks whether the position is on the chessboard or not
        /// </summary>
        /// <param name="position">This is the position we want to know</param>
        /// <returns>This function will return true if there is a cell in that position և that cell is on the chessboard. Otherwise false</returns>
        public bool IsTruePosition(string position)
        {
            return FunctionsForBoard.IsTruePosition(position);
        }
        /// <summary>
        /// This function gives the cell coordinates
        /// </summary>
        /// <param name="position">The position of the cell in which we will find the coordinates</param>
        /// <returns>Int32 as a cell number and Char as a cell letter</returns>
        public (int, char) GetPosition(string position)
        {
            return FunctionsForBoard.GetPosition(position);
        }
        /// <summary>
        /// This function finds the cell from the board according to the cell coordinates
        /// </summary>
        /// <param name="num">The cell number</param>
        /// <param name="letter">The cell Letter</param>
        /// <returns>The cell he found</returns>
        public Cell GetCellByPosition(int num, char letter)
        {
            return FunctionsForBoard.GetCellByPosition(num, letter, this.board);
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
        public void AddKnightToBoard(Cell cell)
        {
            Knight knight = (Knight)this.Figurs[0];
            FunctionsForBoard.AddKnightToBoard(cell, this.board, knight);
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
        public List<Cell> GetKnightRoad(Cell startCell, Cell endCell)
        {
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
        public bool CanFigureMove(Cell startCell, Cell endCell)
        {
            return TwoPlayersLogic.CanFigureMove(startCell, endCell, this.board, FunctionsForBoard);
        }
        /// <summary>
        /// This function checks whether the figure has given a shah to the opponent's king
        /// </summary>
        /// <param name="cell">The figure cell</param>
        /// <returns>True if figure shah to the opponent's king.Otherwise false</returns>
        public bool CheckKingShah(Cell cell)
        {
            return TwoPlayersLogic.CheckKingShah(cell, this.board, FunctionsForBoard);
        }
        #endregion
    }
}