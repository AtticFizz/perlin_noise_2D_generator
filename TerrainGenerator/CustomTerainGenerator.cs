using NoiseGenerator2;
using System.Drawing;
using System.Runtime.Versioning;

namespace TerrainGenerator;

public static class CustomTerrainGenerator
{
    public static double TresholdSea { get; set; }
    public static double TresholdSurface { get; set; }

    public static Color ColorSea { get; set; }
    public static Color ColorSurface { get; set; }
    public static Color ColorMountains { get; set; }

    static CustomTerrainGenerator()
    {
        TresholdSea = 0.95;
        TresholdSurface = 0.5;

        ColorSea = Color.FromArgb(255, 102, 205, 170);
        ColorSurface = Color.FromArgb(255, 107, 142, 35);
        ColorMountains = Color.FromArgb(255, 220, 220, 220);
    }

    [SupportedOSPlatform("windows")]
    public static Bitmap Create()
    {
        double[,] noise = PerlinNoise2D.Create();
        int width = noise.GetLength(0);
        int height = noise.GetLength(1);
        Bitmap noiseImage = new Bitmap(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = ColorMountains;
                if (noise[x, y] <= TresholdSea)
                    pixelColor = ColorSea;
                else if (noise[x, y] <= TresholdSurface)
                    pixelColor = ColorSurface;
                noiseImage.SetPixel(x, y, pixelColor);
            }
        }

        return noiseImage;
    }


}