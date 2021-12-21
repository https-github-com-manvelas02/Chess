using Game;

namespace Chess.UI
{
    /// <summary>
    /// This class is responsible for working with the User Interface
    /// </summary>
    public class ChessUi
    {
        TaskMethods taskMethods;
        bool IsGoWhite = true;
        List<(int, string)> FiguresNames = new List<(int, string)> { (1, "Queen"), (2, "Rook"), (3, "Bishop"), (4, "Knight") };
        string figureName;
        int leftCursor;
        int topCursor;
        /// <summary>
        /// The function is for launch the game menu
        /// </summary>
        public void Start()
        {
            taskMethods = new TaskMethods();
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
        private void History()
        {
            Console.WriteLine(taskMethods.GetHistory());
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
                if (taskMethods.GetCellByPosition(startPos) != null)
                {
                    break;
                }
            }
            taskMethods.CreateOnlyKnight();
            taskMethods.AddKnightToBoard(startPos);
            this.Print();
            while (true)
            {
                Console.Write("\nPlease select target cell : ");
                endPos = Console.ReadLine();
                if (taskMethods.GetCellByPosition(endPos) != null)
                {
                    break;
                }
            }
            List<string> steps = taskMethods.GetKnightRoad(startPos, endPos);
            for (int i = 0; i < steps.Count - 1; i++)
            {
                taskMethods.GetCellByPosition(steps[i]);
                taskMethods.GetCellByPosition(steps[i + 1]);
                if (taskMethods.CanFigureMove(out bool a))
                {
                    Thread.Sleep(1000);
                    this.Print();
                }
                taskMethods.ClearPositions();
            }

            foreach (string item in steps)
            {
                Console.Write($"\n{item}");
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
            bool isMat;
            while (true)
            {
                taskMethods.ClearPositions();
                while (true)
                {
                    while (true)
                    {
                        Console.Write("\nPlease select a figure start position for move : ");
                        startPos = Console.ReadLine();
                        if (taskMethods.GetCellByPosition(startPos) != null)
                        {
                            break;
                        }
                    }
                    if (taskMethods.CheckCellFigure())
                    {
                        if (IsGoWhite)
                        {
                            if (taskMethods.GiveFigureColor())
                            {
                                IsGoWhite = false;
                                break;
                            }
                            Console.WriteLine("Please select the Green Figure Position");
                        }
                        else
                        {
                            if (!taskMethods.GiveFigureColor())
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
                    taskMethods.ClearPositions();
                }
                while (true)
                {
                    Console.Write("Please select target position : ");
                    endPos = Console.ReadLine();
                    if (taskMethods.GetCellByPosition(endPos) != null)
                    {
                        break;
                    }
                    Console.WriteLine("You cant move the selected position please select another position");
                }

                if (taskMethods.CanFigureMove(out isMat))
                {
                    if (taskMethods.IsChangePawn())
                    {
                        Console.WriteLine("\n");
                        leftCursor = Console.CursorLeft;
                        topCursor = Console.CursorTop;
                        figureName = this.SelectChangedFigure();
                        taskMethods.ChangePawn(figureName);
                    }
                    if (isMat)
                    {
                        this.Print();
                        string winerColor;
                        if (IsGoWhite)
                        {
                            winerColor = "Red";
                        }
                        else
                        {
                            winerColor = "Green";
                        }
                        Console.WriteLine("\nThe game is over.Winer is " + winerColor);
                        IsGoWhite = true;
                        this.History();
                        Console.ReadKey();
                        this.Start();
                    }
                    if (taskMethods.IsPat())
                    {
                        this.Print();
                        Console.WriteLine("\nThe game is over.");
                        IsGoWhite = true;
                        Console.ReadKey();
                        this.Start();
                    }
                    this.PlayView();
                }
                if (IsGoWhite)
                {
                    IsGoWhite = false;
                }
                else
                {
                    IsGoWhite = true;
                }
                Console.WriteLine("PLease do all angain");
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
                    if (this.taskMethods.board.Cells[j].Figur != null)
                    {
                        Console.BackgroundColor = this.taskMethods.board.Cells[j].Figur.colorBackgraund;
                    }
                    else
                    {
                        Console.BackgroundColor = (ConsoleColor)this.taskMethods.board.Cells[j].Color;
                    }
                    if (this.taskMethods.board.Cells[j].Figur == null)
                    {
                        toWrite = "  ";
                    }
                    else
                    {
                        toWrite = $"{(this.taskMethods.board.Cells[j].Figur.GetType().Name)[0]}{(this.taskMethods.board.Cells[j].Figur.GetType().Name)[1]}";
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
        private string SelectChangedFigure()
        {
            Console.SetCursorPosition(leftCursor,topCursor);
            string changedFigureName ="";
            Console.WriteLine("Please select a figur to change with your pawn");
            for (int i = 0; i < FiguresNames.Count; i++)
            {
                Console.WriteLine($"{FiguresNames[i].Item1}){FiguresNames[i].Item2}");
            }
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    changedFigureName =  FiguresNames[0].Item2;
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    changedFigureName = FiguresNames[1].Item2;
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    changedFigureName =  FiguresNames[2].Item2;
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    changedFigureName =  FiguresNames[3].Item2;
                    break;
                default:
                    this.SelectChangedFigure();
                    break;
            }
            return changedFigureName;
        }
    }
}
