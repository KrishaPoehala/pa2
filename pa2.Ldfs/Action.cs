
namespace pa2.Ldfs;

public class Queen
{
    public Queen(int x, int y, int wrong)
    {
        X = x;
        Y = y;
        Wrong = wrong;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Wrong { get; set; }

    public void Deconstruct(out int x, out int y, out int wrong)
    {
        x = X;
        y = Y;
        wrong = Wrong;
    }
}
