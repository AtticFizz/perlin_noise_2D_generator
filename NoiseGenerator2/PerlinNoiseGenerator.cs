using System.Drawing;
using System.Runtime.Versioning;

namespace NoiseGeneration;

public class PerlinNoiseGenerator
{
    private int _seed;
    public int Seed
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

    public int Width { get; set; }
    public int Height { get; set; }

    public int Octives { get; set; }
    public double Amplitude { get; set; }
    public double AmplitudeChange { get; set; }
    public double Frequency { get; set; }
    public double FrequencyChange { get; set; }

    private double _currentAmplitude;
    private double _currentFrequency;

    private Random _randomNumberGenerator;
    private List<int> _permutation;

    public delegate double FadeFunction(double t);
    public FadeFunction Fade { get; set; }

    public delegate Vector2 GenerateConstantVectorFunction(double t);
    public GenerateConstantVectorFunction GetConstantVector { get; set; }

    public PerlinNoiseGenerator()
    {
        Width = 256;
        Height = 256;

        Octives = 8;
        Amplitude = 1;
        AmplitudeChange = 0.5;
        Frequency = 0.005;
        FrequencyChange = 2;

        _currentAmplitude = Amplitude;
        _currentFrequency = Frequency;

        _seed = 0;
        _randomNumberGenerator = new Random(0);
        _permutation = MakePermutation();

        Fade = Fade_Default;
        GetConstantVector = GetConstantVector_Default;
    }

    private List<int> MakePermutation()
    {
        List<int> permutation = new List<int>();
        for (int i = 0; i < 256; i++)
            permutation.Add(i);
        permutation.Shuffle(_randomNumberGenerator);
        for (int i = 0; i < 256; i++)
            permutation.Add(permutation[i]);
        return permutation;
    }

    private Vector2 GetConstantVector_Default(double t)
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

    private double Fade_Default(double t)
    {
        return ((6 * t - 15) * t + 10) * t * t * t;
    }

    private double Lerp(double t, double a1, double a2)
    {
        return a1 + t * (a2 - a1);
    }

    public double CreateNoiseValue(double x, double y)
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

        double dotProduct_tr = v_tr.Dot(GetConstantVector(t_tr));
        double dotProduct_tl = v_tl.Dot(GetConstantVector(t_tl));
        double dotProduct_br = v_br.Dot(GetConstantVector(t_br));
        double dotProduct_bl = v_bl.Dot(GetConstantVector(t_bl));
        
        double u = Fade(x_decimal);
        double v = Fade(y_decimal);

        double interpolatedLeft = Lerp(v, dotProduct_bl, dotProduct_tl);
        double interpolatedRight = Lerp(v, dotProduct_br, dotProduct_tr);
        
        return Lerp(u, interpolatedLeft, interpolatedRight);
    }

    public double CreateFractalBrownianMotionValue(double x, double y)
    {
        _currentAmplitude = Amplitude;
        _currentFrequency = Frequency;

        double result = 0;
        for (int octive = 0; octive < Octives; octive++)
        {
            result += _currentAmplitude * CreateNoiseValue(x * _currentFrequency, y * _currentFrequency);
            _currentAmplitude *= AmplitudeChange;
            _currentFrequency *= FrequencyChange;
        }

        return result;
    }

    public double[,] CreateNoise()
    {
        double[,] noise = new double[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                noise[x, y] = CreateFractalBrownianMotionValue(x, y);
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
