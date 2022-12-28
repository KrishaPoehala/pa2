using pa2.Common;

namespace pa2.Ldfs;
public class LdfsSearch
{
	private int _totalWrong = 0;
	private bool _isSolved = false;
	private const int MAX_DEPTH = 32;

	private readonly int _fieldSize;
	private readonly int _queensCount;
	private readonly List<Queen> _queens;
	private readonly int[][] _playfield;
	private readonly TimerService _timer;
	public List<Queen> Queens => _queens;
	public LdfsSearch(int fieldSize, int queensCount)
	{
		_timer = new();
		_queens = new();
		_fieldSize = fieldSize;
		_queensCount = queensCount;
		_playfield = new int[_fieldSize][];
		for (int i = 0; i < _fieldSize; i++)
		{
			_playfield[i] = new int[_fieldSize];
		}
	}

    public List<Queen> Search(List<Queen> initial)
	{
		_timer.Start();
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
				if (AreEqualDirections(x, y, x2, y2))
				{
					_queens[i].Wrong++;
					wrong++;
					_totalWrong++;
				}
			}

			_totalWrong += wrong + 1;
			_queens.Add(new(x, y, wrong + 1));
			add--;
		}
	}

	private void MoveRec(int queen, int x, int y, int depth)
    {
        bool isHit = _playfield[y][x] == 1;
        if (_isSolved || isHit || depth >= MAX_DEPTH)
        {
            return;
        }

		_timer.ThrowIfCancelationRequested();
        var (oldx, oldy, oldw) = _queens[queen];
        UpdateMapAndQueens(queen, x, y, oldx, oldy, oldw, true);
        UpdateQueensIfEqualDirections(queen, x, y, oldx, oldy, out var wrong);
        _queens[queen].Wrong = wrong + 1;
        _totalWrong += wrong + 1;
        if (_totalWrong == _queensCount)
        {
            _isSolved = true;
            return;
        }

        depth++;
        MoveRevAndUpdateDepth(queen, depth);
        if (_isSolved)
        {
            return;
        }

        UpdateMapAndQueens(queen, x, y, oldx, oldy, wrong + 1, false);
        _totalWrong += oldw;
        _queens[queen].Wrong = oldw;
        UpdateQueensIfEqualDirections(queen, oldx, oldy, x, y, out _);
    }

    private void MoveRevAndUpdateDepth(int queen, int depth)
    {
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
    }

    private void UpdateMapAndQueens(int queen, int x, int y, int oldx, int oldy, int oldw, bool isReverse)
    {
        _queens[queen].X = isReverse ? x : oldx;
        _queens[queen].Y = isReverse ? y : oldy;
        _playfield[y][x] = isReverse ? 1 : 0;
        _playfield[oldy][oldx] = isReverse ? 0 : 1;
		_totalWrong -= oldw;
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
