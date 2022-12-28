namespace pa4.Algorithm;

public class Point
{
    public Point(int x, int y, double lMin)
    {
        X = x;
        Y = y;
        LMin = lMin;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public double LMin { get; set; }

    public override string ToString()
    {
        return X + " " + Y + "\n";
    }
}