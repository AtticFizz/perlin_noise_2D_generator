using System.Numerics;
using System;
using NoiseGenerator2;
using TerrainGenerator;

namespace PerlinNoise2D_1;

public partial class View : Form
{
    private static readonly bool DEBUG_VISIBLE = false;

    private Bitmap _image;

    public View()
    {
        InitializeComponent();
        Debug.Initialize(debugTextbox);
        Debug.SetVisibility(DEBUG_VISIBLE);

        PerlinNoise2D.Seed = 1234;
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
        //double[,] noise = PerlinNoise2D.Create();
        //_image = PerlinNoise2D.ToBitmap(noise);

        //double[,] noise = new double[ClientSize.Width, ClientSize.Height];
        //for (int y = 0; y < ClientSize.Height; y++)
        //{
        //    for (int x = 0; x < ClientSize.Width; x++)
        //    {
        //        noise[x, y] = PerlinNoise2D.CreateNoiseValue(x * 0.01, y * 0.01);
        //    }
        //}
        //_image = PerlinNoise2D.ToBitmap(noise);

        if (createOnce)
        {
            CustomTerrainGenerator.TresholdSea = 0.000005;
            CustomTerrainGenerator.TresholdSurface = 0.55;
            _image = CustomTerrainGenerator.Create();
            createOnce = false;
        }

        Refresh();
    }

}