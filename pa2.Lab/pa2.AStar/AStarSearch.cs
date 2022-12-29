﻿using pa2.Common;

namespace pa2.AStar;

public class AStarSearch
{
	private readonly int _queensCount;
	private readonly int _fieldSize;
	private readonly TimerService _timerService;
	public AStarSearch(int queensCount, int fieldSize)
    {
        _queensCount = queensCount;
        _fieldSize = fieldSize;
		_timerService = new TimerService();
    }

    public State? FindThePath(List<Queen> queens)
	{
		UpgradeInitial(queens);
		State? solution = default;
		var isFound = false;
		_timerService.Start();
		while (isFound == false)
		{
			_timerService.ThrowIfCancelationRequested();
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
				solution = ChessMap.States[(int)minIdx].CreateDirevative((int)minIdx,
					_queensCount, _fieldSize);
				if (solution is not null)
				{
					isFound = true;
				}
			}
		}

		return solution;
	}

    private void UpgradeInitial(List<Queen> queens)
    {
		var newState = new State(queens);
		var map = newState.Map = new List<int>(_fieldSize * _fieldSize);
		for (int j = 0; j < _fieldSize * _fieldSize; j++)
		{
			map.Add(0);
		}

		for(int i = 0; i < queens.Count; ++ i)
		{
			var x = queens[i].X;
			var y = queens[i].Y;
			ChessMap.UpgradeMap(map, x, y, 1, _fieldSize);
		}

		newState.GenerateActions(queens.Count, _fieldSize);
		ChessMap.States.Add(newState);
	}

    public List<Queen> GenerateInitialQueens()
	{
		if (!ConsoleHelper.IsInRange((uint)_queensCount, (uint)_fieldSize))
		{
			throw new InvalidDataException();
		}

		var queens = new List<Queen>(_queensCount);


        var i = _queensCount;
        while (i > 0)
        {
            var x = _queensCount - i;
            var y = Random.Shared.Next(0, _fieldSize - 1);
            queens.Add(new(x, y));
            i--;
        }

		return queens;
	}
}