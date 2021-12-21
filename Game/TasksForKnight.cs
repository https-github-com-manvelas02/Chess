using ClassicChess.Entities;
using ClassicChess.Entities.Figurs;
using ClassicChess.Recite;
using ClassicChess.Recite.Colors;

namespace Game
{
    /// <summary>
    /// This class is responsible for working with the Knight
    /// </summary>
    public class TasksForKnight
    {
        Cell knightPossition;
        List<Cell> knightPossiblePositions = new List<Cell>();
        List<(Cell, List<Cell>)> KnightPossibleRoad = new List<(Cell, List<Cell>)>();
        List<Cell> passedKnightPositions = new List<Cell>();

        /// <summary>
        /// This function get the Knight shortest Road
        /// </summary>
        /// <param name="startCell">The Knight Cell</param>
        /// <param name="endCell">The target Cell</param>
        /// <param name="board">The board on which the other arguments are located</param>
        /// <returns>The horse shortest Road List by steps </returns>
        public List<string> GetKnightRoad(Cell startCell, Cell endCell, Board board)
        {
            Cell knightPos = this.GetAllPossiblePosition(startCell, endCell, board);
            int index = KnightPossibleRoad.Count - 1;
            List<string> steps = new List<string>();
            string cell = (char)startCell.Letter + ((int)startCell.Number).ToString();
            steps.Add(cell);

            while (true)
            {
                if (index == -1)
                {
                    break;
                }
                for (int i = 0; i < KnightPossibleRoad[index].Item2.Count; i++)
                {
                    if (KnightPossibleRoad[index].Item2[i] == knightPossition)
                    {
                        cell = (char)KnightPossibleRoad[index].Item1.Letter + ((int)KnightPossibleRoad[index].Item1.Number).ToString();
                        steps.Add(cell);
                        knightPossition = KnightPossibleRoad[index].Item1;
                        break;
                    }
                }
                index--;
            }
            return steps;
        }
        /// <summary>
        /// This function create a knight
        /// </summary>
        /// <returns>The created Knight</returns>
        public Knight CreateOnlyKnight()
        {
            Knight knight = new Knight(FigursColors.Green);
            return knight;
        }
        /// <summary>
        /// This function find all posiible positions start cell to end cell
        /// </summary>
        /// <param name="startCell">The Knight Cell</param>
        /// <param name="endCell">The target Cell</param>
        /// <param name="board">The board on which the other arguments are located</param>
        /// <returns>The last Knight Cell</returns>
        private Cell GetAllPossiblePosition(Cell startCell, Cell endCell, Board board)
        {
            knightPossition = endCell;
            knightPossiblePositions = this.PossiblePositions(knightPossition, board);
            KnightPossibleRoad.Add((knightPossition, knightPossiblePositions));

            for (int i = 0; i < 64; i++)
            {
                knightPossiblePositions = KnightPossibleRoad[i].Item2;
                List<Cell> tempknightPossiblePositions = new List<Cell>();
                for (int j = 0; j < knightPossiblePositions.Count; j++)
                {
                    knightPossition = knightPossiblePositions[j];
                    if (knightPossition == startCell)
                    {
                        knightPossition = startCell;
                        return knightPossition;
                    }
                    tempknightPossiblePositions = this.PossiblePositions(knightPossition, board);

                    KnightPossibleRoad.Add((knightPossition, tempknightPossiblePositions));
                    foreach (Cell item in tempknightPossiblePositions)
                    {
                        if (item == startCell)
                        {
                            knightPossition = item;
                            return knightPossition;
                        }
                    }
                }
            }
            return knightPossition;
        }
        /// <summary>
        /// This function give all possible cells from that cell
        /// </summary>
        /// <param name="cell">The Knight Cell</param>
        /// <param name="board">The board on which the other arguments are located</param>
        /// <returns>All possible cells from that cell</returns>
        private List<Cell> PossiblePositions(Cell cell, Board board)
        {
            passedKnightPositions.Add(cell);
            List<Cell> possiblePositions = new List<Cell>();
            //according to i it is decided to change the number by 2, the letter by 1 or the number by 1 by the letter 2
            //(i in case of 1 the number will be changed by 1 to the letter 2)
            //(in case of i 2 the number will be changed by 2 to the letter 1)
            for (int i = 1; i < 3; i++)
            {
                //according to j it is decided to decrease the number (-) or add (+) depending on the sign of j 
                for (int j = -1; j < 2; j += 2)
                {
                    //according to f it is decided to decrease the letter (-) or add (+) depending on the sign of f
                    for (int f = -1; f < 2; f += 2)
                    {
                        Numbers possibleNumber = (Numbers)((int)cell.Number + i * j);
                        Letters possibleLetters = (Letters)(cell.Letter + (i - i / 2 + i % 2) * f);
                        if (possibleLetters >= (Letters)'A' && possibleLetters <= (Letters)'H' && possibleNumber >= (Numbers)1 && possibleNumber <= (Numbers)8)
                        {
                            if (CanAddPossiblePosition(possibleNumber, possibleLetters))
                            {
                                Cell possibleCell = board.Cells.FirstOrDefault(c => c.Number == possibleNumber && c.Letter == possibleLetters);
                                possiblePositions.Add(possibleCell);
                            }
                        }
                    }
                }
            }
            return possiblePositions;
        }
        /// <summary>
        /// This function determines whether it is a probable cell or not
        /// </summary>
        /// <param name="number">The Cell Number</param>
        /// <param name="letter">The Cell Letter</param>
        /// <returns>True if it has not yet passed that cell. Otherwise false</returns>
        private bool CanAddPossiblePosition(Numbers number, Letters letter)
        {
            for (int i = 0; i < passedKnightPositions.Count; i++)
            {
                if (passedKnightPositions[i].Number == number && passedKnightPositions[i].Letter == letter)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
