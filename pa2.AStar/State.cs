
namespace pa2.AStar;


public class State
{
	public int Depth = 0;
	public List<int> Map = new();
	public List<Action> Actions = new();
	public List<Queen> Queens = new();
	public Action? FirstPreviousAction = null;

	public bool IsSolution(int queensCount)
	{
		for (int q = 0; q < queensCount; q++)
		{
			int x = Queens[q].X;
			int y = Queens[q].Y;
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
			int x = Queens[q].X;
			int y = Queens[q].Y;
			for (int i = 0; i < fieldSize; i++)
			{
				if (y != i)
				{
					Actions.Add(new(Map[i * 8 + x], q, x, i));
				}
			}
		}

		Actions.Sort((x, y) => x.HitsCount - y.HitsCount);
	}

	public State? CreateDirevative(int index, int queensCount, int fieldSize)
	{
		var action = Actions[0];
		Actions.RemoveAt(0);
		var newState = new State();
		newState.Depth = Depth + 1;
		newState.FirstPreviousAction = action;
		foreach (var item in Queens)
		{
			newState.Queens.Add(new(item.X, item.Y));
		}

		newState.Map = new(Map);
		ChessMap.UpgradeMap(newState.Map, newState.Queens[action.QueenNumber], -1, fieldSize);
		newState.Queens[action.QueenNumber].X = action.Ox;
		newState.Queens[action.QueenNumber].Y = action.Oy;
		var sorted = new List<Queen>(newState.Queens);

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

		if (Actions.Count == 0)
		{
			ChessMap.States.RemoveAt(index);
		}

		return null;
	}
}
