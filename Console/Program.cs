using System;
using System.Drawing.Text;
using NoiseGenerator2;

namespace ConsoleApp;

internal static class Application
{
    private static void Main(string[] args)
    {
        int width = 7;
        int height = 7;
        double[,] noise = new double[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noise[x, y] = PerlinNoise2D.CreateNoiseValue(x * 1, y * 1);
            }
        }
        PrintNoise(noise, width, height);
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
