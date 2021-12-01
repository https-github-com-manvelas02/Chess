using ClassicChess.Enums;
using ClassicChess.Enums.Colors;

namespace ClassicChess.Classes.Figurs.Interface
{
    public interface IFigure
    {
        Numbers Number { get; set; }
        Letters Letter { get; set; }
        FigursColors Color { get; }
        bool IsMove(Cell cell);
        bool IsSamePos(Cell cell);
    }
}
