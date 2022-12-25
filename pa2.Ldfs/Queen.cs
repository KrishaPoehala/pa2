
using pa2.Common;

namespace pa2.Ldfs;

public class Queen : QueenBase
{
    public Queen(int x, int y, int wrong)
    {
        X = x;
        Y = y;
        Wrong = wrong;
    }

    public int Wrong { get; set; }

    public void Deconstruct(out int x, out int y, out int wrong)
    {
        x = X;
        y = Y;
        wrong = Wrong;
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
