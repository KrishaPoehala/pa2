namespace pa5.Algorithm;

public class Point
{
    public Point(int x, int y, int path)
    {
        X = x;
        Y = y;
        Path = path;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Path { get; set; }
}