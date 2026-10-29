using System;
namespace CatandMouseGame;
class Program
{
    static void Main(string[] args)
    {
        string baseDir = AppContext.BaseDirectory;

        for (int i = 1; i <= 3; i++)
        {
            string inFile = Path.Combine(baseDir, $"{i}.ChaseData.txt");
            string outFile = Path.Combine(baseDir, $"{i}.PursuitLog.txt");
            if (!File.Exists(inFile))
            {
                Console.WriteLine("Skip: {0} not found", inFile);
                continue;
            }
            int size = int.Parse(File.ReadAllLines(inFile)[0].Trim());
            Game.InputFile = inFile;
            Game.OutFile = outFile;
            var game = new Game(size);
            game.Run();
            Console.WriteLine("Test {0} done -> {1}", i, outFile);
        }
    }
}
