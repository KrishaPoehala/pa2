
namespace pa2.Ldfs;

public class ConsoleHelper
{
    public static void Draw(List<Queen> queens, int fieldSize)
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                if (queens.Any(q => q.X == j && q.Y == i))
                {
                    Console.Write("Q");
                }
                else
                {
                    Console.Write("*");
                }

                Console.Write('\t');
            }

            Console.WriteLine('\n');
        }
    }
    public static (int, int) GetDataFromConsole()
    {
        Console.WriteLine("Welcome to the lab 2");
        while (true)
        {
            Console.WriteLine("Enter the queens count: ");
            var queensAsString = Console.ReadLine();
            Console.WriteLine("Enter the field size: ");
            var fieldSizeAsString = Console.ReadLine();
            if (IsNullOrWhiteSpace(queensAsString, fieldSizeAsString))
            {
                continue;
            }

            if (!uint.TryParse(queensAsString?.Trim(), out var queensCount))
            {
                Console.WriteLine($"Wrong format of {queensAsString}");
            }

            if (!uint.TryParse(fieldSizeAsString?.Trim(), out var fieldSize))
            {
                Console.WriteLine($"Wrong format of {fieldSizeAsString}");
            }

            if (IsInRange(queensCount, fieldSize))
            {
                return ((int)queensCount, (int)fieldSize);
            }

            Console.WriteLine($"Max values are: {Constants.FIELD_SIZE_MAX}" +
                $" and {Constants.QUEENS_COUNT_MAX}");
        }
    }

    public static bool IsInRange(uint queensCount, uint fieldSize)
    {
        bool queensInRange = queensCount >= Constants.QUEENS_COUNT_MIN &&
            queensCount <= Constants.FIELD_SIZE_MAX;
        bool fieldSizeInRange = fieldSize >= Constants.FIELD_SIZE_MIN &&
            fieldSize <= Constants.FIELD_SIZE_MAX;
        return queensInRange && fieldSizeInRange;
    }

    private static bool IsNullOrWhiteSpace(params string?[] values)
    {
        return values.Any(x => string.IsNullOrWhiteSpace(x));
    }
}
