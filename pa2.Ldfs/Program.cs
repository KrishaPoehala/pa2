
using pa2.Ldfs;

var (queensCount, fieldSize) = ConsoleHelper.GetDataFromConsole();
var ldfs = new LdfsSearch(queensCount, fieldSize);
ldfs.GenerateInitialQueens();
Console.WriteLine("The generated board: ");
ConsoleHelper.Draw(ldfs.Queens, 6);
var solution = ldfs.Search();
Console.WriteLine("The found solution");
ConsoleHelper.Draw(solution, 6);
