namespace pa5.Algorithm;
public class AntsGraphSearch
{

	private List<Point> points;
	private double[,] distances;
	private double[,] feramones;
    private Configuration _conf;
    public AlgorithmResults Results { get; private set; }

    public double[,] Distances => distances;
    public double[,] Pheramones => feramones;
    public List<Point> Points => points;
    public int PointsCount => _conf.PointsCount;
    public AntsGraphSearch()
    {
        Results = new();
        _conf = new();
        Init();
    }

    public AntsGraphSearch(Configuration configuration)
    {
        Results = new();
        _conf = configuration;
        Init();
    }

    public void Init()
    {
        points = new();
        Results.ShortestDistance = double.MaxValue;
        Results.Iterations = 0;
        points = new(_conf.PointsCount);
        distances = new double[_conf.PointsCount, _conf.PointsCount];
        feramones = new double[_conf.PointsCount, _conf.PointsCount];
        GeneratePoints();
        CalculateInitialDistancesAndPheramones();
    }

    public void UpdateScore()
    {
        Results.CurrentScore += Results.InitialShortestDistance - Results.ShortestDistance;
    }

    private void GeneratePoints()
    {
        for (var i = 0; i < _conf.PointsCount; i++)
        {
            var pos = new Point(Random.Shared.Next(0, 800), Random.Shared.Next(0, 800), -1);
            points.Add(pos);
        }
    }

    private void CalculateInitialDistancesAndPheramones()
    {
        for (var i = 0; i < _conf.PointsCount; i++)
        {
            for (var j = 0; j < _conf.PointsCount; j++)
            {
                var dx = points[i].X - points[j].X;
                var dy = points[i].Y - points[j].Y;
                var dist = Math.Sqrt(dx * dx + dy * dy);
                distances[i, j] = dist;
                feramones[i, j] = 1;
            }
        }
    }


    public void UpdateRoads()
    {
        var pheramonesAdd = new double[_conf.PointsCount, _conf.PointsCount];
        for (var i = 0; i < _conf.AntsCount; i++)
        {
            var walked = 0d;
            var startPos = Random.Shared.Next(_conf.PointsCount);
            var currentPos = startPos;
            var visited = new bool[_conf.PointsCount];
            visited[currentPos] = true;
            var path = new List<int>
            {
                currentPos
            };

            for (var k = 1; k <= _conf.PointsCount; k++)
            {
                int newPos = GetNextPos(startPos, currentPos, visited, k);
                walked += distances[currentPos, newPos];
                var add = 1 / walked;
                pheramonesAdd[currentPos, newPos] += add;
                pheramonesAdd[newPos, currentPos] += add;
                visited[newPos] = true;
                path.Add(newPos);
                currentPos = newPos;
            }

            UpdateShortestDistance(walked, path);
        }

        UpdatePheramones(pheramonesAdd);
    }

    private int GetNextPos(int startPos, int curPos, bool[] visited, int k)
    {
        var newPos = 0;
        if (k < _conf.PointsCount)
        {
            var chances = CalculateChances(curPos, visited);
            double chanceSum = chances.Sum();
            var chanceRanges = CalculateChancesRanges(chances, chanceSum);
            newPos = GetNewPos(chanceRanges);
        }
        else
        {
            newPos = startPos;
        }

        return newPos;
    }

    private int GetNewPos(List<double> chanceRanges)
    {
        var newPos = 0;
        var random = Random.Shared.NextDouble();
        while (newPos < _conf.PointsCount && random > chanceRanges[newPos])
        {
            newPos++;
        }
        newPos--;
        return newPos;
    }

    private List<double> CalculateChancesRanges(List<double> chances, double chanceSum)
    {
        var chanceRanges = new List<double>();
        for (var j = 0; j < _conf.PointsCount; j++)
        {
            chances[j] /= chanceSum;
            if (j == 0)
            {
                chanceRanges.Add(0);
            }
            else
            {
                chanceRanges.Add(chanceRanges[j - 1] + chances[j - 1]);
            }
        }

        return chanceRanges;
    }

    private List<double> CalculateChances(int curPos, bool[] visited)
    {
        var chances = new List<double>();
        for (var j = 0; j < _conf.PointsCount; j++)
        {
            if (visited[j])
            {
                chances.Add(0);
            }
            else
            {
                var chance = Math.Pow(feramones[curPos, j], _conf.Alpha)
                    * Math.Pow(1 / distances[curPos, j], _conf.Beta);
                chances.Add(chance);
            }   
        }

        return chances;
    }

    private void UpdateShortestDistance(double walked, List<int> path)
    {
        if (walked < Results.ShortestDistance)
        {
            Results.ShortestDistance = walked;
            for (var j = 1; j < path.Count; j++)
            {
                points[path[j - 1]].Path = path[j];
            }
        }
    }

    private void UpdatePheramones(double[,] pheramonesAdd)
    {
        for (var i = 0; i < _conf.PointsCount; i++)
        {
            for (var j = 0; j < _conf.PointsCount; j++)
            {
                feramones[i, j] = feramones[i, j] * (1 - _conf.Ro) + pheramonesAdd[i, j];
            }
        }

        if (Results.Iterations == 0)
        {
            Results.InitialShortestDistance = Results.ShortestDistance;
        }
    }
}
