using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NoiseGenerator2;

public static class PerlinNoise2D
{
    private static int _seed;
    public static int Seed
    {
        get
        {
            return _seed;
        }
        set
        {
            _seed = value;
            _randomNumberGenerator = new Random(_seed);
            _permutation = MakePermutation();
        }
    }

    private static Random _randomNumberGenerator;
    private static List<int> _permutation;

    static PerlinNoise2D()
    {
        _randomNumberGenerator = new Random(0);
        _permutation = MakePermutation();
    }

    private static void Shuffle(this List<int> list)
    {
        for (int i = list.Count - 1; i > 1; i--)
        {
            int index = _randomNumberGenerator.Next(i + 1);
            int value = list[index];
            list[index] = list[i];
            list[i] = value;
        }
    }

    private static List<int> MakePermutation()
    {
        List<int> permutation = new List<int>();
        for (int i = 0; i < 256; i++)
            permutation.Add(i);
        permutation.Shuffle();
        for (int i = 0; i < 256; i++)
            permutation.Add(permutation[i]);
        return permutation;
    }

    private static Vector2 GetConstatntVector(double t)
    {
        double t_wrapped = t % 3;
        if (t_wrapped == 0)
            return new Vector2(1, 1);
        else if (t_wrapped == 1)
            return new Vector2(-1, 1);
        else if (t_wrapped == 2)
            return new Vector2(-1, -1);
        return new Vector2(1, -1);
    }

    private static double Fade(double t)
    {
        return ((6 * t - 15) * t + 10) * t * t * t;
    }

    private static double Lerp(double t, double a1, double a2)
    {
        return a1 + t * (a2 - a1);
    }

    public static double CreateNoiseValue(double x, double y)
    {   
        int x_corner = (int)Math.Floor(x) % 255;
        int y_corner = (int)Math.Floor(y) % 255;

        int t_tr = _permutation[_permutation[x_corner + 1] + y_corner + 1];
        int t_tl = _permutation[_permutation[x_corner] + y_corner + 1];
        int t_br = _permutation[_permutation[x_corner + 1] + y_corner];
        int t_bl = _permutation[_permutation[x_corner] + y_corner];

        double x_decimal = x - Math.Floor(x);
        double y_decimal = y - Math.Floor(y);

        Vector2 v_tr = new Vector2(x_decimal - 1, y_decimal - 1);
        Vector2 v_tl = new Vector2(x_decimal, y_decimal - 1);
        Vector2 v_br = new Vector2(x_decimal - 1, y_decimal);
        Vector2 v_bl = new Vector2(x_decimal, y_decimal);

        double dotProduct_tr = v_tr.Dot(GetConstatntVector(t_tr));
        double dotProduct_tl = v_tl.Dot(GetConstatntVector(t_tl));
        double dotProduct_br = v_br.Dot(GetConstatntVector(t_br));
        double dotProduct_bl = v_bl.Dot(GetConstatntVector(t_bl));
        
        double u = Fade(x_decimal);
        double v = Fade(y_decimal);

        double interpolatedLeft = Lerp(v, dotProduct_bl, dotProduct_tl);
        double interpolatedRight = Lerp(v, dotProduct_br, dotProduct_tr);
        
        return Lerp(u, interpolatedLeft, interpolatedRight);
    }

    public static double CreateFractalBrownianMotionValue(double x, double y, int octives)
    {
        double result = 0;
        double amplitude = 1;
        double frequency = 0.005;

        for (int octive = 0; octive < octives; octive++)
        {
            double t = amplitude * CreateNoiseValue(x * frequency, y * frequency);
            result += t;
            amplitude *= 0.5;
            frequency *= 2;
        }

        return result;
    }

    public static double[,] Create()
    {
        double[,] noise = new double[256, 256];

        for (int y = 0; y < 256; y++)
        {
            for (int x = 0; x < 256; x++)
            {
                noise[x, y] = CreateFractalBrownianMotionValue(x, y, 8);
            }
        }

        return noise;
    }

    [SupportedOSPlatform("windows")]
    public static Bitmap ToBitmap(double[,] noise)
    {
        int width = noise.GetLength(0);
        int height = noise.GetLength(1);
        Bitmap noiseImage = new Bitmap(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                byte value = (byte)noise[x, y].Remap(-1, 1, 0, 255);
                Color pixelColor = Color.FromArgb(255, value, value, value);
                noiseImage.SetPixel(x, y, pixelColor);
            }
        }

        return noiseImage;
    }
}
