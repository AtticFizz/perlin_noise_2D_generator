namespace FormsApp;

internal static class Renderer
{
    public static void Draw(Bitmap image, PaintEventArgs e)
    {
        e.Graphics.DrawImageUnscaled(image, 0, 0);
    }

}
