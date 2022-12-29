namespace pa4.Algorithm;

public interface IRandom
{
    int Next(int min, int max);
    Point GetRandomPoint();
    double NextDouble();
}