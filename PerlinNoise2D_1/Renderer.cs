using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerlinNoise2D_1;

internal static class Renderer
{
    public static void Draw(Bitmap image, PaintEventArgs e)
    {
        e.Graphics.DrawImageUnscaled(image, 0, 0);
    }

}
