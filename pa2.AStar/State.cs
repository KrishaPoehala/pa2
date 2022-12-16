
namespace pa2.AStar;


public class State
{
	private int _depth = 0;
	public List<int> Map { get; internal set; } = new();
	private List<Action> _actions = new();
	private List<Queen> _queens = new();

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
	public bool IsSolution(int queensCount)
	{
		for (int q = 0; q < queensCount; q++)
		{
			int x = _queens[q].X;
			int y = _queens[q].Y;
			if (Map[y * 8 + x] >= 2)
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
					_actions.Add(new(Map[i * 8 + x], q, x, i));
				}
			}
		}

		_actions.Sort((x, y) => x.HitsCount - y.HitsCount);
	}

	public State? CreateDirevative(int index, int queensCount, int fieldSize)
	{
		var action = _actions[0];
		_actions.RemoveAt(0);
		var newState = new State();
		newState._depth = _depth + 1;
		foreach (var item in _queens)
		{
			newState._queens.Add(new(item.X, item.Y));
		}

		newState.Map = new(Map);
		ChessMap.UpgradeMap(newState.Map, newState._queens[action.QueenNumber], -1, fieldSize);
		newState._queens[action.QueenNumber].X = action.Ox;
		newState._queens[action.QueenNumber].Y = action.Oy;
		var sorted = new List<Queen>(newState._queens);

		sorted.Sort((q, b) => (q.X + q.Y * fieldSize)
		- (b.X + b.Y * fieldSize));

		string hash = string.Join("-", sorted);
		if (!ChessMap.Set.Contains(hash))
		{
			ChessMap.Set.Add(hash);
			ChessMap.UpgradeMap(newState.Map, action.Ox, action.Oy, 1, fieldSize);
			if (newState.IsSolution(fieldSize))
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
}
