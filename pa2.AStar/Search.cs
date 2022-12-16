namespace pa2.AStar;

public static class Search
{
	public static State FindThePath(List<Queen> queens,int queensCount, int fieldSize)
	{
		UpgradeInitial(queens, fieldSize);
		while (true)
		{
			int? minIdx = null;
			var minVal = int.MaxValue;
			for (var i = 0; i < ChessMap.States.Count; i++)
			{
				var state = ChessMap.States[i];
				var val = state.Depth + state.Actions[0].HitsCount;
				if (val < minVal)
				{
					minVal = val;
					minIdx = i;
				}
			}

			if (minIdx is not null)
			{
				var possibleSolution = ChessMap.States[(int)minIdx].CreateDirevative((int)minIdx,
					queensCount, fieldSize);
				if (possibleSolution is not null)
				{
					return possibleSolution;
				}
			}
		}
	}

    private static void UpgradeInitial(List<Queen> queens, int fieldSize)
    {
		var newState = new State(queens);
		var map = newState.Map = new List<int>(8 * 8);
		for (int j = 0; j < 64; j++)
		{
			map.Add(0);
		}

		for(int i = 0; i < queens.Count; ++ i)
		{
			var x = queens[i].X;
			var y = queens[i].Y;
			ChessMap.UpgradeMap(map, x, y, 1, fieldSize);
		}

		newState.GenerateActions(queens.Count, fieldSize);
		ChessMap.States.Add(newState);
	}

    public static List<Queen> GenerateInitialQueens(int queensCount, int fieldSize)
	{
		if (!ConsoleHelper.IsInRange((uint)queensCount, (uint)fieldSize))
		{
			throw new InvalidDataException();
		}

		var queens = new List<Queen>(queensCount);


        var i = queensCount;
        while (i > 0)
        {
            var x = queensCount - i;
            var y = Random.Shared.Next(0, fieldSize - 1);
            queens.Add(new(x, y));
            i--;
        }

		return queens;
	}
}