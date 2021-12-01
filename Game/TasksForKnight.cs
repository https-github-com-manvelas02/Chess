using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace Game
{
    public class TasksForKnight
    {

        Cell knightPossition;
        List<Cell> knightPossiblePositions = new List<Cell>();
        List<(Cell, List<Cell>)> KnightPossibleRoad = new List<(Cell, List<Cell>)>();
        List<Cell> passedKnightPositions = new List<Cell>();
        public List<Cell> GetKnightRoad(Cell startCell, Cell endCell, Board board)
        {
            int index;
            Cell knightPos = this.GetAllPossiblePosition(startCell, endCell, board, out index);
            List<Cell> steps = new List<Cell>();
            steps.Add(startCell);
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
                        steps.Add(KnightPossibleRoad[index].Item1);
                        knightPossition = KnightPossibleRoad[index].Item1;
                        break;
                    }
                }
                index--;
            }
            return steps;
        }
        private Cell GetAllPossiblePosition(Cell startCell, Cell endCell, Board board, out int index)
        {
            knightPossition = endCell;
            knightPossiblePositions = this.PossiblePositions(knightPossition, board);
            KnightPossibleRoad.Add((knightPossition, knightPossiblePositions));

            index = 0;

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
                    index++;
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
        private List<Cell> PossiblePositions(Cell cell, Board board)
        {
            passedKnightPositions.Add(cell);
            List<Cell> possiblePositions = new List<Cell>();
            //yst i-i voroshvuma tivy 2-ov popoxvi tar-y 1-ov te tivy 1-ov tary 2-ov
            //(i-n 1-i depqum tivy popoxvum e 1-ov tary 2-ov)
            //(i-n 2-i depqum tivy popoxvum e 2-ov tary 1-ov)
            for (int i = 1; i < 3; i++)
            {
                //yst j-i voroshvuma tivy pakasacnenq(-) te avelacnenq(+) kaxvac nshanic 
                for (int j = -1; j < 2; j += 2)
                {
                    //yst f-i voroshum enq tary pakasacnenq(-) te avelacnenq(+) kaxvac nshanic
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

        public Knight CreateOnlyKnight()
        {
            Knight knight = new Knight(FigursColors.Green);
            return knight;
        }

    }
}
