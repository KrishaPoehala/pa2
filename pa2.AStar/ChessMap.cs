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
        UpdateStraigthLines(map, x, y, value, fieldSize);
        UpdateDiagonals(map, x, y, value, fieldSize);
    }

    private static void UpdateDiagonals(List<int> map, int x, int y, int value, int fieldSize)
    {
        int z = y - x;
        for (int i = 0; i < fieldSize; i++)
        {
            if (x != i && z + i >= 0 && z + i < fieldSize)
            {
                map[(z + i) * fieldSize + i] += value;
            }
        }

        z = y + x;
        for (int i = 0; i < fieldSize; i++)
        {
            if (x != i && z - i >= 0 && z - i < fieldSize)
            {
                map[(z - i) * fieldSize + i] += value;
            }
        }
    }

    private static void UpdateStraigthLines(List<int> map, int x, int y, int value, int fieldSize)
    {
        for (int i = 0; i < fieldSize; i++)
        {
            if (x != i)
            {
                map[y * fieldSize + i] += value;
            }
        }

        for (int i = 0; i < fieldSize; i++)
        {
            if (y != i)
            {
                map[i * fieldSize + x] += value;
            }
        }
    }
}

