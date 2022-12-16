using System.Diagnostics.CodeAnalysis;

namespace pa2.AStar;

public class QueenEqualityComparer : IEqualityComparer<Queen>
{
    public bool Equals(Queen? left, Queen? right)
    {
        if (left == null && right == null)
        {
            return false;

        }

        if (left?.X == right?.X && left?.Y == right?.Y)
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
