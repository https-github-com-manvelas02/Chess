using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Enums.Colors;
using Game;

namespace Chess.UI
{
    /// <summary>
    /// This class is responsible for working with the User Interface
    /// </summary>
    public class ChessUi
    {
        TaskMethods taskMethods = new TaskMethods();
        bool IsGoWhite = true;
        bool IsShah = false;
        FigursColors figursColors;

        /// <summary>
        /// The function is for launch the game menu
        /// </summary>
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("1)PLay\t2)Knight\tESC)Exit");
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    this.PlayView();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    this.PlayWithKnight();
                    break;
                default:
                    this.Start();
                    break;
            }
        }
        /// <summary>
        /// The function is starting for play with only knight
        /// </summary>
        private void PlayWithKnight()
        {
            this.Print();
            string startPos;
            string endPos;
            while (true)
            {
                Console.Write("\nPlease select start cell for  knight : ");
                startPos = Console.ReadLine();
                if (taskMethods.IsTruePosition(startPos))
                {
                    break;
                }
            }
            (int, char) cellPosition = taskMethods.GetPosition(startPos);
            Cell startCell = taskMethods.GetCellByPosition(cellPosition.Item1, cellPosition.Item2);
            taskMethods.CreateOnlyKnight();
            taskMethods.AddKnightToBoard(startCell);
            this.Print();
            while (true)
            {
                Console.Write("\nPlease select target cell : ");
                endPos = Console.ReadLine();
                if (taskMethods.IsTruePosition(endPos))
                {
                    break;
                }
            }
            cellPosition = taskMethods.GetPosition(endPos);
            Cell endCell = taskMethods.GetCellByPosition(cellPosition.Item1, cellPosition.Item2);
            List<Cell> steps = taskMethods.GetKnightRoad(startCell, endCell);
            for (int i = 0; i < steps.Count - 1; i++)
            {
                if (taskMethods.CanFigureMove(steps[i], steps[i + 1]))
                {
                    Thread.Sleep(1000);
                    this.Print();
                }
            }

            foreach (Cell item in steps)
            {
                Console.Write($"\n{item.Letter}{(int)item.Number}");
            }
            Console.WriteLine($"\nThe total moves count is : {steps.Count - 1}");
        }
        /// <summary>
        /// This function launches two players game process
        /// </summary>
        private void PlayView()
        {
            if (this.taskMethods.Figurs.Count == 0)
            {
                this.taskMethods.CreateFigires();
                this.taskMethods.AddFiguresOnBoard();
            }
            this.Print();
            string startPos;
            string endPos;
            Cell startCell;
            (int, char) position;
            while (true)
            {
                while (true)
                {
                    Console.Write("\nPlease select a figure start position for move : ");
                    startPos = Console.ReadLine();
                    if (taskMethods.IsTruePosition(startPos))
                    {
                        break;
                    }
                }
                position = taskMethods.GetPosition(startPos);
                startCell = taskMethods.GetCellByPosition(position.Item1, position.Item2);
                if (startCell.Figur != null)
                {
                    if (IsGoWhite)
                    {
                        if (startCell.Figur.Color == FigursColors.Green)
                        {
                            IsGoWhite = false;
                            break;
                        }
                        Console.WriteLine("Please select the Green Figure Position");
                    }
                    else
                    {
                        if (startCell.Figur.Color == FigursColors.Red)
                        {
                            IsGoWhite = true;
                            break;
                        }
                        Console.WriteLine("Please select the Red Figure Position");
                    }
                }
                else
                {
                    Console.WriteLine("please select the position where there is a figure");
                }
            }
            while (true)
            {
                while (true)
                {
                    Console.Write("Please select target position : ");
                    endPos = Console.ReadLine();
                    if (taskMethods.IsTruePosition(endPos))
                    {
                        break;
                    }
                }
                position = taskMethods.GetPosition(endPos);
                Cell endCell = taskMethods.GetCellByPosition(position.Item1, position.Item2);
                if (taskMethods.CanFigureMove(startCell, endCell))
                {
                    IsShah = this.taskMethods.CheckKingShah(endCell);
                    if (IsShah)
                    {
                        figursColors = endCell.Figur.Color;
                    }
                    this.PlayView();
                }
                Console.WriteLine("You cant move the selected position please select another position");
            }
        }
        /// <summary>
        /// The function prints chess board and figures
        /// </summary>
        private void Print()
        {
            Console.Clear();
            for (int i = 0; i < this.taskMethods.board.Width / 2; i++)
            {
                Console.Write($"{this.taskMethods.board.Numbers[i]}|");
                for (int j = (i * 8); j < ((i * 8) + 8); j++)
                {
                    string toWrite;
                    Console.BackgroundColor = (ConsoleColor)this.taskMethods.board.Cells[j].Color;
                    if (this.taskMethods.board.Cells[j].Figur == null)
                    {
                        toWrite = "  ";
                    }
                    else if (this.taskMethods.board.Cells[j].Figur.GetType() == typeof(King))
                    {
                        toWrite = $"{(this.taskMethods.board.Cells[j].Figur.GetType().Name)[0]}{(this.taskMethods.board.Cells[j].Figur.GetType().Name)[1]}";
                        Console.ForegroundColor = (ConsoleColor)this.taskMethods.board.Cells[j].Figur.Color;
                        if (IsShah)
                        {
                            if (this.taskMethods.board.Cells[j].Figur.Color != figursColors)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                            }
                        }
                    }
                    else
                    {
                        toWrite = $"{(this.taskMethods.board.Cells[j].Figur.GetType().Name)[0]} ";
                        Console.ForegroundColor = (ConsoleColor)this.taskMethods.board.Cells[j].Figur.Color;
                    }
                    Console.Write(toWrite);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.Write("  ");
            for (int i = 0; i < this.taskMethods.board.Letters.Count; i++)
            {
                Console.Write($"{this.taskMethods.board.Letters[i]} ");
            }
        }
    }
}
