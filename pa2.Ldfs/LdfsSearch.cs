namespace pa2.Ldfs;
public class LdfsSearch
{
	private int _totalWrong = 0;
	private bool _isSolved = false;
	private readonly int _maxDepth = 32;
	private readonly List<Queen> _queens = new();
	private readonly int[][] _playfield = new int[8][];

	public List<Queen> Queens => _queens;
	public int[][] PlayField => _playfield;

	private readonly int _fieldSize;
	private readonly int _queensCount;

	public LdfsSearch(int fieldSize, int queensCount)
	{
		_fieldSize = fieldSize;
		_queensCount = queensCount;

		for (int i = 0; i < 8; i++)
		{
			_playfield[i] = new int[8];
		}
	}

	public List<Queen> Search(List<Queen> initial)
	{
		UpgradeInitial(initial);
		for (var q = 0; q < _queensCount; q++)
		{
			var x = _queens[q].X;
			var y = _queens[q].Y;
			for (var i = 0; i < _fieldSize; i++)
			{
				if (y != i)
				{
					MoveRec(q, x, i, 0);
				}
			}
		}

		if (_isSolved)
		{
			return _queens;
		}

		throw new Exception("The solution was not found");
	}


	public List<Queen> GenerateInitialQueens()
    {
		var queens = new List<Queen>();
        for (int i = 1; i <= _queensCount; i++)
        {
			var x = _queensCount - i;
			var y = Random.Shared.Next(0, _fieldSize - 1);
			queens.Add(new Queen(x, y, 0));
        }

		return queens;
    }

	private void UpgradeInitial(List<Queen> initialQueens)
	{
		var add = _queensCount - 1;
		while (add >= 0)
		{
			var x = initialQueens[add].X;
			var y = initialQueens[add].Y;
			_playfield[y][x] = 1;
			var wrong = 0;
			for (var i = 0; i < _queens.Count; i++)
			{
				var x2 = _queens[i].X;
				var y2 = _queens[i].Y;
				if (x == x2 || y == y2 || x + y == x2 + y2 || x - y == x2 - y2)
				{
					_queens[i].Wrong++;
					wrong++;
					_totalWrong++;
				}
			}

			_totalWrong += wrong + 1;
            Console.Write(wrong + 1 + "\t");
			_queens.Add(new(x, y, wrong + 1));
			add--;
		}
	}

	private void MoveRec(int queen, int x, int y, int depth)
	{
		if (depth >= _maxDepth || _isSolved)
		{
			return;
		}

		if (_playfield[y][x] == 1)
		{
			return;
		}

		var (oldx, oldy, oldw) = _queens[queen];
		_queens[queen].X = x;
		_queens[queen].Y = y;
		_playfield[y][x] = 1;
		_playfield[oldy][oldx] = 0;
		_totalWrong -= oldw;
		UpdateQueensIfEqualDirections(queen, x, y, oldx, oldy, out var wrong);
		_queens[queen].Wrong = wrong + 1;
		_totalWrong += wrong + 1;
		if (_totalWrong == _queensCount)
		{
			_isSolved = true;
			return;
		}

		depth++;
		for (var q = 0; q < _queensCount; q++)
		{
			var (x1, y1, _) = _queens[q];
			if (q == queen)
			{
				continue;
			}

			for (var i = 0; i < _fieldSize; i++)
			{
				if (y1 != i)
				{
					MoveRec(q, x1, i, depth);
				}
			}
		}

		if (_isSolved)
		{
			return;
		}

		_queens[queen].X = oldx;
		_queens[queen].Y = oldy;
		_queens[queen].Wrong = oldw;
		_playfield[y][x] = 0;
		_playfield[oldy][oldx] = 1;
		_totalWrong -= wrong + 1;
		_totalWrong += oldw;


		UpdateQueensIfEqualDirections(queen, oldx, oldy, x, y, out _);
	}

	private void UpdateQueensIfEqualDirections(int queen, int x0, int y0, int x1, int y1, out int wrong)
	{
		wrong = 0;
		for (var i = 0; i < _queensCount; i++)
		{
			if (i == queen)
			{
				continue;
			}

			var (currentX, currentY, _) = _queens[i];
			if (AreEqualDirections(x0, y0, currentX, currentY))
			{
				_queens[i].Wrong++;
				++wrong;
				_totalWrong++;
			}

			if (AreEqualDirections(x1, y1, currentX, currentY))
			{
				_queens[i].Wrong--;
				_totalWrong--;
			}
		}
	}
	private static bool AreEqualDirections(int x1, int y1, int x2, int y2) =>
			x1 == x2 || y1 == y2 || x1 + y1 == x2 + y2 || x1 - y1 == x2 - y2;
}
