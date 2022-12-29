﻿namespace pa2.AStar;
public class State
{
	private int _depth = 0;
	public List<int> Map { get; internal set; } = new();
	private readonly List<Action> _actions = new();
	private readonly List<Queen> _queens = new();

    public State()
    {
    }

    public State(List<Queen> queens)
    {
        _queens = queens;
    }

    public int Depth => _depth;
	public List<Action> Actions => _actions;
	public List<Queen > Queens => _queens;
	public bool IsSolution(int queensCount, int fieldSize)
	{
		for (int q = 0; q < queensCount; q++)
		{
			int x = _queens[q].X;
			int y = _queens[q].Y;
			if (Map[y * fieldSize + x] >= 2)
			{
				return false;
			}
		}

		return true;
	}

	public void GenerateActions(int queensCount, int fieldSize)
	{
		for (int q = 0; q < queensCount; q++)
		{
			int x = _queens[q].X;
			int y = _queens[q].Y;
			for (int i = 0; i < fieldSize; i++)
			{
				if (y != i)
				{
					_actions.Add(new(Map[i * fieldSize + x], q, x, i));
				}
			}
		}

		_actions.Sort((x, y) => x.HitsCount - y.HitsCount);
	}

	public State? CreateDirevative(int index, int queensCount, int fieldSize)
    {
        var action = _actions[0];
        _actions.RemoveAt(0);
        var newState = GetNextState(fieldSize, action);
        var hash = GenerateUniqueHash(newState, fieldSize);
        if (!ChessMap.Set.Contains(hash))
        {
            ChessMap.Set.Add(hash);
            ChessMap.UpgradeMap(newState.Map, action.Ox, action.Oy, 1, fieldSize);
            if (newState.IsSolution(queensCount, fieldSize))
            {
                return newState;
            }

            newState.GenerateActions(queensCount, fieldSize);
            ChessMap.States.Add(newState);
        }

        if (_actions.Count == 0)
        {
            ChessMap.States.RemoveAt(index);
        }

        return null;
    }

    private string GenerateUniqueHash(State newState, int fieldSize)
    {
        var sorted = new List<Queen>(newState._queens);
        sorted.Sort((q, b) => (q.X + q.Y * fieldSize)
        - (b.X + b.Y * fieldSize));
        string hash = string.Join("-", sorted);
        return hash;
    }

    private State GetNextState(int fieldSize, Action action)
    {
        var newState = new State { _depth = _depth + 1 };
        foreach (var item in _queens)
        {
            newState._queens.Add(new(item.X, item.Y));
        }

        newState.Map = new(Map);
        ChessMap.UpgradeMap(newState.Map, newState._queens[action.QueenNumber], -1, fieldSize);
        newState._queens[action.QueenNumber].X = action.Ox;
        newState._queens[action.QueenNumber].Y = action.Oy;
        return newState;
    }
}
