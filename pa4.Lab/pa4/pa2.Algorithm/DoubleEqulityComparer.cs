using System.Diagnostics.CodeAnalysis;

namespace pa4.Algorithm;

public class DoubleEqualityComparer : IEqualityComparer<double>
{
    const double EPSILON = 0.00000001;
    public bool Equals(double x, double y)
    {
        var diff = Math.Abs(x - y);
        if (diff <= EPSILON)
        {
            return true;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] double obj)
    {
        return obj.GetHashCode();
    }
}