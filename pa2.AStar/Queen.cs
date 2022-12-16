using pa2.Common;

namespace pa2.AStar;

public class Queen: QueenBase
{
    public Queen(int x, int y)
    {
        X = x;
        Y = y;
    }


    public override string ToString()
    {
        return $"{X},{Y}";
    }
}

