using System;
using System.IO;

namespace CatandMouseGame;

class Program
{
    static void Main(string[] args)
    {
        string baseDir = AppContext.BaseDirectory;

        while (true)
        {
            Console.Write("Введите номер файла для обработки (или 'exit' для выхода): ");
            string input = Console.ReadLine();

            if (input?.ToLower() == "exit") break;
            if (string.IsNullOrWhiteSpace(input)) continue;

            string inFile = Path.Combine(baseDir, $"{input}.ChaseData.txt");
            string outFile = Path.Combine(baseDir, $"{input}.PursuitLog.txt");

            if (!File.Exists(inFile))
            {
                Console.WriteLine($"Файл {inFile} не найден! Попробуйте еще раз.");
                continue;
            }

            int size = int.Parse(File.ReadAllLines(inFile)[0].Trim());
            Game.InputFile = inFile;
            Game.OutFile = outFile;
            var game = new Game(size);
            game.Run();

            Console.WriteLine($"Тест {input} выполнен -> {outFile}\n");
        }
    }
}
