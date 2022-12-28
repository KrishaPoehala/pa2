namespace pa4.Algorithm;

public class RandomService : IRandom
{
    private readonly Random _random = new();
    public Point GetRandomPoint() => new(_random.Next(0, 700), _random.Next(0, 700), 0);

    public int Next(int min, int max) => _random.Next(min, max);

    public double NextDouble() => _random.NextDouble();
}