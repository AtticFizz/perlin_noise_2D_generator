using NoiseGeneration;
using System.Drawing;
using System.Runtime.Versioning;

namespace TerrainGeneration;

public class TerrainGenerator
{
    public PerlinNoiseGenerator PerlinNoiseGenerator { get; set; }

    public double TresholdSea { get; set; }
    public double TresholdSurface { get; set; }

    public Color ColorSea { get; set; }
    public Color ColorSurface { get; set; }
    public Color ColorMountains { get; set; }

    public TerrainGenerator()
    {
        PerlinNoiseGenerator = new PerlinNoiseGenerator();

        TresholdSea = 0.95;
        TresholdSurface = 0.5;

        ColorSea = Color.FromArgb(255, 102, 205, 170);
        ColorSurface = Color.FromArgb(255, 107, 142, 35);
        ColorMountains = Color.FromArgb(255, 220, 220, 220);
    }

    [SupportedOSPlatform("windows")]
    public Bitmap Create()
    {
        double[,] noise = PerlinNoiseGenerator.CreateNoise();
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