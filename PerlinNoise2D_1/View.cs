using System.Numerics;
using System;
using NoiseGeneration;

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

    private void framerateTimer_Tick(object sender, EventArgs e)
    {
        // _image = UV.Create(ClientSize.Width, ClientSize.Height);
        // _image = PerlinNoise2D.Create(1, ClientSize.Width, ClientSize.Height);
        double[,] noise = PerlinNoise2D.Create(1, ClientSize.Width, ClientSize.Height, 10, 10);
        _image = PerlinNoise2D.ToBitmap(noise);
        //Debug.LogLine(_image.GetPixel(0, 0));
        Refresh();
    }
}