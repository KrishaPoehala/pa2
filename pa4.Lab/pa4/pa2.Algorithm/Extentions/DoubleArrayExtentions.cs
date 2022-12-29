namespace pa4.Algorithm.Extentions;

public static class DoubleArrayExtentions
{
    public static List<double> ToList(this double[,] arr)
    {
        var list = new List<double>();
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                list.Add(arr[i, j]);
            }
        }

        return list;
    }
}