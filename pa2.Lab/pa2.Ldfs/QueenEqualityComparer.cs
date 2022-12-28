using System.Diagnostics.CodeAnalysis;

namespace pa2.Ldfs;

public class QueenEqualityComparer : IEqualityComparer<Queen>
{
    public bool Equals(Queen? left, Queen? right)
    {
        if (left is null || right is null)
        {
            return false;
        }

        if (left.X == right.X && right.Y == right.Y)
        {
            return true;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] Queen obj)
    {
        return obj.GetHashCode();
    }
}
