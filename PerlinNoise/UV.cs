using System.Drawing;
using System.Runtime.Versioning;

namespace NoiseGeneration;

public static class UV
{
    [SupportedOSPlatform("windows")]
    public static Bitmap Create(int width, int height)
    {
        Bitmap noiseImage = new Bitmap(width, height);

        float widthRatio = width / (float)255;
        float heightRatio = height / (float)255;

        for (int y = 0; y < height; y++)
        {
            byte r = (byte)(y / heightRatio);
            for (int x = 0; x < width; x++)
            {
                if (y == 0)
                {
                    byte g = (byte)(x / widthRatio);
                    Color pixelColor = Color.FromArgb(255, r, g, 0);
                    noiseImage.SetPixel(x, y, pixelColor);
                }
                else
                {
                    byte g = noiseImage.GetPixel(x, 0).G;
                    Color pixelColor = Color.FromArgb(255, r, g, 0);
                    noiseImage.SetPixel(x, y, pixelColor);
                }
            }
        }

        return noiseImage;
    }
}
