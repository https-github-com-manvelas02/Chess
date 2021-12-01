﻿using ClassicChess.Classes;
using ClassicChess.Classes.Figurs;
using ClassicChess.Classes.Figurs.Interface;
using ClassicChess.Enums.Colors;

namespace Game
{
    public class TwoPlayersLogic
    {
        public bool CanFigureMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> Road = new List<Cell>();

            IFigure figur = startCell.Figur;
            IFigure newFigur = null;
            if (figur.IsMove(endCell))
            {
                if (figur.GetType() == typeof(Knight))
                {
                    newFigur = new Knight(startCell.Figur.Color);
                    newFigur.Number = endCell.Number;
                    newFigur.Letter = endCell.Letter;
                    board.Cells.FirstOrDefault(cell => cell == endCell).Figur = newFigur;
                    board.Cells.FirstOrDefault(cell => cell == startCell).Figur = null;
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
                /*else if(figur.GetType() == typeof(King))
                {
                    if (this.CanKingMove(startCell,endCell,board,boardFunctions))
                    {

                    }
                }*/
                for (int i = 0; i < Road.Count; i++)
                {
                    if (Road[i].Figur != null)
                    {
                        return false;
                    }
                }
                newFigur.Number = endCell.Number;
                newFigur.Letter = endCell.Letter;
                board.Cells.FirstOrDefault(cell => cell == endCell).Figur = newFigur;
                board.Cells.FirstOrDefault(cell => cell == startCell).Figur = null;
                return true;
            }
            return false;
        }
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
        private List<Cell> BishopMoveRoad(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<Cell> bishopRoad = new List<Cell>();
            int startNum;
            int endNum;
            char startLet;
            char endLet;
            char j;
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
                for (int i = startNum; i <= endNum; i++)
                {
                    Cell cell = boardFunctions.GetCellByPosition(i, startLet, board);
                    bishopRoad.Add(cell);
                    startLet++;
                }
            }
            return bishopRoad;
        }
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
                for (int i = startNum; i < endNum; i++)
                {
                    Cell cell = boardFunctions.GetCellByPosition(i, let, board);
                    rookRoad.Add(cell);
                }
                return rookRoad;
            }
        }
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
        /*private bool CanKingMove(Cell startCell, Cell endCell, Board board, FunctionsForBoard boardFunctions)
        {
            List<IFigure> figures = new List<IFigure>();
            FigursColors figursColors = startCell.Figur.Color;
            if (endCell.Figur == null)
            {

            }
        }*/
        private List<IFigure> GetFigures(Board board, FigursColors figursColors)
        {
            List<IFigure> figures = new List<IFigure>();
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Figur.Color != figursColors)
                {
                    figures.Add(board.Cells[i].Figur);
                }
            }
            return figures;
        }
    }
}