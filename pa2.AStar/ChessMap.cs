namespace pa2.AStar;

public static class ChessMap
{
	public static HashSet<string> Set = new();
	public static List<State> States = new();
	public static void UpgradeMap(List<int> map, Queen queen, int value, int fieldSize)
	{
		UpgradeMap(map, queen.X, queen.Y, value, fieldSize);
	}

	public static void UpgradeMap(List<int> map, int x, int y, int value, int fieldSize)
	{

		map[y * 8 + x] += value;
		for (int i = 0; i < fieldSize; i++)
		{
			if (x != i)
			{
				map[y * 8 + i] += value;
			}
		}

		for (int i = 0; i < fieldSize; i++)
		{
			if (y != i)
			{
				map[i * 8 + x] += value;
			}
		}

		int z = y - x;
		for (int i = 0; i < fieldSize; i++)
		{
			if (x != i && z + i >= 0 && z + i < fieldSize)
			{
				map[(z + i) * 8 + i] += value;
			}
		}

		z = y + x;
		for (int i = 0; i < fieldSize; i++)
		{
			if (x != i && z - i >= 0 && z - i < fieldSize)
			{
				map[(z - i) * 8 + i] += value;
			}
		}
	}
}

