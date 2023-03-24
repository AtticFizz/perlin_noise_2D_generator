using System.Numerics;
using System;
using NoiseGeneration;
using TerrainGeneration;

namespace FormsApp;

public partial class View : Form
{
    private static readonly bool DEBUG_VISIBLE = false;

    private PerlinNoiseGenerator PerlinNoiseGenerator;
    private TerrainGenerator TerrainGenerator;

    private Bitmap _image;

    public View()
    {
        InitializeComponent();
        Debug.Initialize(debugTextbox);
        Debug.SetVisibility(DEBUG_VISIBLE);

        PerlinNoiseGenerator = new PerlinNoiseGenerator();
        TerrainGenerator = new TerrainGenerator();

        _image = UV.Create(ClientSize.Width, ClientSize.Height);
    }

    private void View_Load(object sender, EventArgs e)
    {
        DoubleBuffered = true;
        Paint += OnPaint;
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        Renderer.Draw(_image, e);
    }

    private bool createOnce = true;

    private void framerateTimer_Tick(object sender, EventArgs e)
    {
        // CreateNoise();
        CreateTerrain();
        Refresh();
    }

    private void CreateNoise()
    {
        if (createOnce)
        {
            PerlinNoiseGenerator.Seed = 69420;
            PerlinNoiseGenerator.Width = ClientSize.Width;
            PerlinNoiseGenerator.Height = ClientSize.Height;
            PerlinNoiseGenerator.Octives = 8;
            PerlinNoiseGenerator.Amplitude = 0.9;
            PerlinNoiseGenerator.AmplitudeChange = 0.5;
            PerlinNoiseGenerator.Frequency = 0.007;
            PerlinNoiseGenerator.FrequencyChange = 2;
            double[,] noise = PerlinNoiseGenerator.CreateNoise();
            _image = PerlinNoiseGenerator.ToBitmap(noise);
            createOnce = false;
        }
    }

    private void CreateTerrain()
    {
        if (createOnce)
        {
            TerrainGenerator.TresholdSea = 0.000005;
            TerrainGenerator.TresholdSurface = 0.55;

            TerrainGenerator.PerlinNoiseGenerator.Seed = 6942069;
            TerrainGenerator.PerlinNoiseGenerator.Width = ClientSize.Width;
            TerrainGenerator.PerlinNoiseGenerator.Height = ClientSize.Height;
            TerrainGenerator.PerlinNoiseGenerator.Octives = 8;
            TerrainGenerator.PerlinNoiseGenerator.Amplitude = 0.9;
            TerrainGenerator.PerlinNoiseGenerator.AmplitudeChange = 0.5;
            TerrainGenerator.PerlinNoiseGenerator.Frequency = 0.007;
            TerrainGenerator.PerlinNoiseGenerator.FrequencyChange = 2;

            _image = TerrainGenerator.Create();
            createOnce = false;
        }
    }

}