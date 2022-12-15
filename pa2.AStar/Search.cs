namespace pa2.AStar;

public static class Search
{
	public static State FindThePath(int queensCount, int fieldSize)
	{
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

	public static List<Queen> GenerateInitialQueens(int queensCount, int fieldSize)
	{
		if (!ConsoleHelper.IsInRange((uint)queensCount, (uint)fieldSize))
		{
			throw new InvalidDataException();
		}

		var newState = new State();
		var queens = newState.Queens;
		var map = newState.Map = new List<int>(8 * 8);
		for (int j = 0; j < 64; j++)
		{
			map.Add(0);
		}

		var i = queensCount;
		while (i > 0)
		{
			var x = queensCount - i;
			var y = Random.Shared.Next(0, fieldSize - 1);

			ChessMap.UpgradeMap(map, x, y, 1, fieldSize);
			queens.Add(new(x, y));
			i--;
		}

		newState.GenerateActions(queensCount, fieldSize);
		ChessMap.States.Add(newState);
		return queens;

	}


}

