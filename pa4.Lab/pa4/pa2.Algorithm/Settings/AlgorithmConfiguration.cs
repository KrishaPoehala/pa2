namespace pa4.Algorithm.Settings;

public class AlgorithmConfiguration
{
    public AlgorithmConfiguration(double a, double b, double p, int pointsCount, int antsCount)
    {
        A = a;
        B = b;
        P = p;
        PointsCount = pointsCount;
        AntsCount = antsCount;
    }

    public double A { get; }
    public double B { get; }
    public double P { get; }
    public int PointsCount { get; }
    public int AntsCount { get; }
}