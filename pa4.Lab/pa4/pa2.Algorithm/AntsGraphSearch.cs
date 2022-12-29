using pa4.Algorithm.Settings;
namespace pa4.Algorithm;

public class AntsGraphSearch
{
    private readonly List<Point> _points = new();
    private readonly double[,] _distances;
    private readonly double[,] _pheramones;
    private readonly double Lmin = 0;
    private readonly AlgorithmConfiguration _configuration;
    private readonly IRandom _random;

    public AntsGraphSearch(IRandom random, AlgorithmConfiguration configuration)
    {
        _random = random;
        _configuration = configuration;
        _points = GeneratePoints();
        _distances = new double[_configuration.PointsCount, _configuration.PointsCount];
        _pheramones = new double[_configuration.PointsCount, _configuration.PointsCount];
        GenerateInitialPheramones();
        Lmin = GetLMin();
    }

    public List<Point> Points => _points;
    public double[,] Pheramones => _pheramones;

    private List<Point> GeneratePoints()
    {
        var points = new List<Point>();
        for (var i = 0; i < _configuration.PointsCount; i++)
        {
            var pos = _random.GetRandomPoint();
            points.Add(pos);
        }

        return points;
    }

    private void GenerateInitialPheramones()
    {
        for (var i = 0; i < _points.Count; i++)
        {
            for (var j = 0; j < _configuration.PointsCount; j++)
            {
                if (i == j)
                {
                    _distances[i, j] = 0;
                    _pheramones[i, j] = 0d;
                    continue;
                }

                var dx = _points[i].X - _points[j].X;
                var dy = _points[i].Y - _points[j].Y;
                var dist = Math.Sqrt(dx * dx + dy * dy);
                _distances[i, j] = dist;
                _pheramones[i, j] = 0.1;
            }
        }
    }

    private double GetLMin()
    {
        double lmin = 0;
        var now = 0;
        for (var i = 0; i < _configuration.PointsCount - 1; i++)
        {
            var minDist = int.MaxValue;
            var minIdx = 0;
            for (var j = 1; j < _configuration.PointsCount; j++)
            {
                if (_points[j].LMin != 0)
                {
                    continue;
                }

                var dx = _points[now].X - _points[j].X;
                var dy = _points[now].Y - _points[j].Y;
                var dist = dx * dx + dy * dy;
                if (dist < minDist)
                {
                    minDist = dist;
                    minIdx = j;
                }
            }

            lmin += Math.Sqrt(minDist);
            now = minIdx;
            _points[now].LMin = Lmin;
        }

        return lmin;
    }

    public void UpdateRoades()
    {
        var pheramoneList = new double[_configuration.PointsCount, _configuration.PointsCount];
        for (var i = 0; i < _configuration.AntsCount; i++)
        {
            var startPos = _random.Next(0, _configuration.PointsCount);
            var curPos = startPos;
            var visited = new bool[_configuration.PointsCount];
            visited[startPos] = true;

            for (var k = 1; k <= _configuration.PointsCount; k++)
            {
                var newPos = 0;
                if (k < _configuration.PointsCount)
                {
                    var chances = GetPheramonesChances(curPos, visited);
                    var chancesSum = chances.Sum();
                    var chanceRanges = GetChancesRanges(chances, chancesSum);
                    var random = _random.NextDouble();
                    newPos = chanceRanges
                            .TakeWhile(x => newPos < _configuration.PointsCount && random > x)
                            .Count() - 1;
                }
                else
                {
                    newPos = startPos;
                }

                var add = Lmin / _distances[curPos, newPos];
                pheramoneList[curPos, newPos] += add;
                pheramoneList[newPos, curPos] += add;
                visited[newPos] = true;
                curPos = newPos;
            }
        }

        UpdatePheramones(pheramoneList);
    }

    private void UpdatePheramones(double[,] pheramoneList)
    {
        for (var i = 0; i < _configuration.PointsCount; i++)
        {
            for (var j = 0; j < _configuration.PointsCount; j++)
            {
                if (i == j)
                {
                    continue;
                }

                _pheramones[i, j] = _pheramones[i, j] * (1 - _configuration.P) + pheramoneList[i, j];
            }
        }
    }

    private List<double> GetChancesRanges(List<double> chances, double chancesSum)
    {
        var chanceRanges = new List<double> { 0 };
        for (var j = 1; j < _configuration.PointsCount; j++)
        {
            chances[j] /= chancesSum;
            chanceRanges.Add(chanceRanges[j - 1] + chances[j - 1]);
        }

        return chanceRanges;
    }

    private List<double> GetPheramonesChances(int curPos, bool[] visited)
    {
        var chances = new List<double>();
        for (var j = 0; j < _configuration.PointsCount; j++)
        {
            if (visited[j])
            {
                chances.Add(0);
                continue;
            }

            if (_pheramones[curPos, j] == 0 || _distances[curPos, j] == 0)
            {
                chances.Add(0);
                continue;
            }

            var chance = Math.Pow(_pheramones[curPos, j], _configuration.A)
                * (1 / Math.Pow(_distances[curPos, j], _configuration.B));
            chances.Add(chance);
        }

        return chances;
    }
}