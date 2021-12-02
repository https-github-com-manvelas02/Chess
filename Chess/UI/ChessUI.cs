using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using Game;

namespace Chess.UI
{
    public class ChessUi
    {
        TaskMethods taskMethods = new TaskMethods();
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
                    Thread.Sleep(2000);
                    this.Print();
                }
            }

            foreach (Cell item in steps)
            {
                Console.Write($"\n{item.Letter}{(int)item.Number}");
            }
        }

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
                    break;
                }
                Console.WriteLine("please select the position where there is a figure");
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
                    this.PlayView();
                }
                Console.WriteLine("You cant move the selected position please select another position");
            }

        }

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
