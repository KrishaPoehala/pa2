namespace pa2.AStar;

public static class ChessMap
{
	public static HashSet<string> Set { get; } = new();
	public static List<State> States { get; } = new();
	public static void UpgradeMap(List<int> map, Queen queen, int value, int fieldSize)
	{
		UpgradeMap(map, queen.X, queen.Y, value, fieldSize);
	}

	public static void UpgradeMap(List<int> map, int x, int y, int value, int fieldSize)
    {
        map[y * fieldSize + x] += value;
        int diff = y - x;
        int sum = y + x;
        for (int i = 0; i < fieldSize; i++)
        {
            if (x != i && diff + i >= 0 && diff + i < fieldSize)
            {
                map[(diff + i) * fieldSize + i] += value;
            }

            if (x != i && sum - i >= 0 && sum - i < fieldSize)
            {
                map[(sum - i) * fieldSize + i] += value;
            }

            if (x != i)
            {
                map[y * fieldSize + i] += value;
            }

            if (y != i)
            {
                map[i * fieldSize + x] += value;
            }
        }
    }
}

