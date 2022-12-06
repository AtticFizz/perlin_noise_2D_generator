using System;
using NoiseGeneration;

namespace ConsoleApp;

internal static class Application
{
    private static void Main(string[] args)
    {
        int width = 8;
        int height = 8;

        double[,] noiseTopLeft = PerlinNoise2D.CreateTopLeft(1, width, height, 2, 2);
        double[,] noiseTopRight = PerlinNoise2D.CreateTopRight(1, width, height, 2, 2);
        double[,] noiseBottomLeft = PerlinNoise2D.CreateBottomLeft(1, width, height, 2, 2);
        double[,] noiseBottomRight = PerlinNoise2D.CreateBottomRight(1, width, height, 2, 2);
        double[,] noise = PerlinNoise2D.Create(1, width, height, 2, 2);

        Console.WriteLine("Top left:");
        PrintNoise(noiseTopLeft, width, height);
        Console.WriteLine("\nTop right:");
        PrintNoise(noiseTopRight, width, height);
        Console.WriteLine("\nCombined:");
        PrintNoise(noise, width, height);
        Console.WriteLine("\nBottom left:");
        PrintNoise(noiseBottomLeft, width, height);
        Console.WriteLine("\nBottom right:");
        PrintNoise(noiseBottomRight, width, height);
    }

    private static void PrintNoise(double[,] noise, int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(noise[x, y].ToString() + "    ");
            }
            Console.WriteLine();
        }
    }
}
