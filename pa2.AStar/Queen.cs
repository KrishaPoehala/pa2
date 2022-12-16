namespace pa2.AStar;

public class Queen
{
    public int X;
    public int Y;
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

