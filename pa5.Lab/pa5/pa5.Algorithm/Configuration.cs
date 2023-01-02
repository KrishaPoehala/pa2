namespace pa5.Algorithm;

public class Configuration
{
    public Configuration()
    {}

    public Configuration(int antsCount, int pointsCount, double ro, double alpha, double beta)
    {
        AntsCount = antsCount;
        PointsCount = pointsCount;
        Ro = ro;
        Alpha = alpha;
        Beta = beta;
    }

    public int AntsCount { get; set; } = 20;
    public int PointsCount { get; set; } = 60;
    public double Ro { get; set; } = 0.3;
    public double Alpha { get; set; } = 3;
    public double Beta { get; set; } = 2;
}
