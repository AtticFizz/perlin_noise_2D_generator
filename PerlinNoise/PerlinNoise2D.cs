using System.Drawing;
using System.Numerics;
using System.Runtime.Versioning;

namespace NoiseGeneration;

public static class PerlinNoise2D
{
    public static double[,] CreateTopLeft(int seed, int width, int height, int chunksX, int chunksY)
    {
        Random random = new Random(seed);
        Vector2 chunkSize = new Vector2(width / chunksX, height / chunksY);
        Vector2[,] influenceVectors = new Vector2[chunksX + 1, chunksY + 1];
        Vector2[,] influencePositions = new Vector2[chunksX + 1, chunksY + 1];

        for (int y = 0; y < chunksY + 1; y++)
        {
            for (int x = 0; x < chunksX + 1; x++)
            {
                Vector2 vector = new Vector2(0, 1);
                double rotation = random.Next(0, 359) + random.NextDouble();
                influenceVectors[x, y] = vector.Rotate(rotation);
                influencePositions[x, y] = new Vector2(x * chunkSize.x, y * chunkSize.y);
            }
        }

        double[,] noiseTopLeft = new double[width, height];

        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noiseTopLeft = CreateGradient(noiseTopLeft, x1, x2, y1, y2, influenceVectors[x, y], influencePositions[x, y]);
            }
        }

        return noiseTopLeft;
    }

    public static double[,] CreateTopRight(int seed, int width, int height, int chunksX, int chunksY)
    {
        Random random = new Random(seed);
        Vector2 chunkSize = new Vector2(width / chunksX, height / chunksY);
        Vector2[,] influenceVectors = new Vector2[chunksX + 1, chunksY + 1];
        Vector2[,] influencePositions = new Vector2[chunksX + 1, chunksY + 1];

        for (int y = 0; y < chunksY + 1; y++)
        {
            for (int x = 0; x < chunksX + 1; x++)
            {
                Vector2 vector = new Vector2(0, 1);
                double rotation = random.Next(0, 359) + random.NextDouble();
                influenceVectors[x, y] = vector.Rotate(rotation);
                influencePositions[x, y] = new Vector2(x * chunkSize.x, y * chunkSize.y);
            }
        }

        double[,] noiseTopRight = new double[width, height];

        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noiseTopRight = CreateGradient(noiseTopRight, x1, x2, y1, y2, influenceVectors[x + 1, y], influencePositions[x + 1, y]);
            }
        }

        return noiseTopRight;
    }

    public static double[,] CreateBottomLeft(int seed, int width, int height, int chunksX, int chunksY)
    {
        Random random = new Random(seed);
        Vector2 chunkSize = new Vector2(width / chunksX, height / chunksY);
        Vector2[,] influenceVectors = new Vector2[chunksX + 1, chunksY + 1];
        Vector2[,] influencePositions = new Vector2[chunksX + 1, chunksY + 1];

        for (int y = 0; y < chunksY + 1; y++)
        {
            for (int x = 0; x < chunksX + 1; x++)
            {
                Vector2 vector = new Vector2(0, 1);
                double rotation = random.Next(0, 359) + random.NextDouble();
                influenceVectors[x, y] = vector.Rotate(rotation);
                influencePositions[x, y] = new Vector2(x * chunkSize.x, y * chunkSize.y);
            }
        }

        double[,] noiseBottoMLeft = new double[width, height];

        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noiseBottoMLeft = CreateGradient(noiseBottoMLeft, x1, x2, y1, y2, influenceVectors[x, y + 1], influencePositions[x, y + 1]);
            }
        }

        return noiseBottoMLeft;
    }


    public static double[,] CreateBottomRight(int seed, int width, int height, int chunksX, int chunksY)
    {
        Random random = new Random(seed);
        Vector2 chunkSize = new Vector2(width / chunksX, height / chunksY);
        Vector2[,] influenceVectors = new Vector2[chunksX + 1, chunksY + 1];
        Vector2[,] influencePositions = new Vector2[chunksX + 1, chunksY + 1];

        for (int y = 0; y < chunksY + 1; y++)
        {
            for (int x = 0; x < chunksX + 1; x++)
            {
                Vector2 vector = new Vector2(0, 1);
                double rotation = random.Next(0, 359) + random.NextDouble();
                influenceVectors[x, y] = vector.Rotate(rotation);
                influencePositions[x, y] = new Vector2(x * chunkSize.x, y * chunkSize.y);
            }
        }

        double[,] noiseBottoMRight = new double[width, height];

        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noiseBottoMRight = CreateGradient(noiseBottoMRight, x1, x2, y1, y2, influenceVectors[x + 1, y + 1], influencePositions[x + 1, y + 1]);
            }
        }

        return noiseBottoMRight;
    }

    //public static double[,] Create(int seed, int width, int height, int chunksX, int chunksY)
    //{
    //    Random random = new Random(seed);
    //    double[,] noise = new double[width, height];
    //    Vector2 chunkSize = new Vector2(width / chunksX, height/ chunksY);

    //    for (int y = 0; y < chunksY; y++)
    //    {
    //        for (int x = 0; x < chunksX; x++)
    //        {
    //            Vector2 vector = new Vector2(0, 1);
    //            double rotation = random.Next(0, 359) + random.NextDouble();
    //            Vector2 influenceVector = vector.Rotate(rotation);
    //            Vector2 influencePosition = new Vector2(x * chunkSize.x, y * chunkSize.y);
    //            int x1 = (int)chunkSize.x * x;
    //            int x2 = x1 + (int)chunkSize.x;
    //            int y1 = (int)chunkSize.y * y;
    //            int y2 = y1 + (int)chunkSize.y;
    //            noise = CreateGradient(noise, x1, x2, y1, y2, influenceVector, influencePosition);
    //        }
    //    }

    //    return noise;
    //}

    public static double[,] Create(int seed, int width, int height, int chunksX, int chunksY)
    {
        Random random = new Random(seed);
        double[,] noise = new double[width, height];
        Vector2 chunkSize = new Vector2(width / chunksX, height / chunksY);
        Vector2[,] influenceVectors = new Vector2[chunksX + 1, chunksY + 1];
        Vector2[,] influencePositions = new Vector2[chunksX + 1, chunksY + 1];

        for (int y = 0; y < chunksY + 1; y++)
        {
            for (int x = 0; x < chunksX + 1; x++)
            {
                Vector2 vector = new Vector2(0, 1);
                double rotation = random.Next(0, 359) + random.NextDouble();
                influenceVectors[x, y] = vector.Rotate(rotation);
                influencePositions[x, y] = new Vector2(x * chunkSize.x, y * chunkSize.y);
            }
        }

        double[,] noiseTopLeft = new double[width, height];
        double[,] noiseTopRight = new double[width, height];
        double[,] noiseBottomLeft = new double[width, height];
        double[,] noiseBottomRight = new double[width, height];
        double[,] noiseTop = new double[width, height];
        double[,] noiseBottom = new double[width, height];

        double[,] noiseCombinedTopLeft = new double[width, height];
        double[,] noiseCombinedTopRight = new double[width, height];
        double[,] noiseCombinedBottomLeft = new double[width, height];
        double[,] noiseCombinedBottomRight = new double[width, height];

        //double[,] noiseTopLeft = CreateGradientMap(noise, chunksX, chunksY, chunkSize, influenceVectors, influencePositions, 0, 0);
        //double[,] noiseTopRight = CreateGradientMap(noise, chunksX, chunksY, chunkSize, influenceVectors, influencePositions, 1, 0);
        //double[,] noiseBottomLeft = CreateGradientMap(noise, chunksX, chunksY, chunkSize, influenceVectors, influencePositions, 0, 1);
        //double[,] noiseBottomRight = CreateGradientMap(noise, chunksX, chunksY, chunkSize, influenceVectors, influencePositions, 1, 1);

        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++) // somethings wrong
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noiseTopLeft = CreateGradient(noiseTop, x1, x2, y1, y2, influenceVectors[x, y], influencePositions[x, y]);
                noiseTopRight = CreateGradient(noiseTop, x1, x2, y1, y2, influenceVectors[x + 1, y], influencePositions[x + 1, y]);
                noiseTop = LerpHorizontal(noiseTopLeft, noiseTopRight, x1, x2, y1, y2);
                // bottom lerping incorrect
                //noiseBottomLeft = CreateGradient(noiseBottom, x1, x2, y1, y2, influenceVectors[x, y + 1], influencePositions[x, y + 1]);
                //noiseBottomRight = CreateGradient(noiseBottom, x1, x2, y1, y2, influenceVectors[x + 1, y + 1], influencePositions[x + 1, y + 1]);
                //noiseBottom = LerpHorizontal(noiseBottomLeft, noiseBottomRight, x1, x2, y1, y2);
                //noiseTop = LerpVertical(noiseTop, noiseBottom, x1, x2, y1, y2);
            }
        }

        return noiseTop;
    }

    private static double[,] CreateGradientMap(double[,] noise, int chunksX, int chunksY, Vector2 chunkSize, Vector2[,] influenceVectors, Vector2[,] influencePositions, int offsetX, int offsetY)
    {
        for (int y = 0; y < chunksY; y++)
        {
            for (int x = 0; x < chunksX; x++)
            {
                int x1 = (int)chunkSize.x * x;
                int x2 = x1 + (int)chunkSize.x;
                int y1 = (int)chunkSize.y * y;
                int y2 = y1 + (int)chunkSize.y;
                noise = CreateGradient(noise, x1, x2, y1, y2, influenceVectors[x + offsetX, y + offsetY], influencePositions[x + offsetX, y + offsetY]);
            }
        }

        return noise;
    }

    private static double[,] CreateGradient(double[,] noise, int x1, int x2, int y1, int y2, Vector2 influenceVector, Vector2 influencePosition)
    {
        for (int y = y1; y < y2; y++)
        {
            for (int x = x1; x < x2; x++)
            {
                Vector2 offsetVector = new Vector2(influencePosition.x - x, influencePosition.y - y).Normalize();
                noise[x, y] = offsetVector.Dot(influenceVector);

                //if (x == 0 && y == 0)
                //{
                //    Console.WriteLine("influencePosition: {" + influencePosition.x + ", " + influencePosition.y + "}");
                //    Console.WriteLine("offsetVector: {" + offsetVector.x + ", " + offsetVector.y + "}");
                //    Console.WriteLine("dotProduct: " + offsetVector.Dot(influenceVector));
                //    Console.WriteLine("noise[0, 0]: " + noise[x, y]);
                //    Console.WriteLine();
                //}
            }
        }

        return noise;
    }

    private static double[,] LerpHorizontal(double[,] noise1, double[,] noise2, int x1, int x2, int y1, int y2)
    {
        int pointCount = x2 - x1 - 1;

        for (int y = y1; y < y2; y++)
        {
            for (int x = x1 + 1; x < x2; x++)
            {
                //Console.WriteLine("x: " + x + " y: " + y);
                //Console.WriteLine("noise2[x, y]: " + noise2[x, y]);
                //Console.WriteLine("noise1[x, y]: " + noise1[x, y]);

                double length = noise2[x, y] - noise1[x, y];
                int pointIndex = x - x1;
                double offset = (pointIndex / (double)pointCount) * length;
                if (offset < 0)
                    noise1[x, y] = noise2[x, y] - offset;
                else
                    noise1[x, y] += offset;

                //Console.WriteLine("length: " + length);
                //Console.WriteLine("pointIndex: " + pointIndex);
                //Console.WriteLine("pointCount: " + pointCount);
                //Console.WriteLine("offset: " + offset);
                //Console.WriteLine("noise1[" + x + ", " + y + "]: " + noise1[x, y]);
                //Console.WriteLine("---");
            }
        }
        //Console.WriteLine();

        return noise1;
    }

    private static double[,] LerpVertical(double[,] noise1, double[,] noise2, int x1, int x2, int y1, int y2)
    {
        int pointCount = x2 - x1 - 1;

        for (int x = x1; x < x2; x++)
        {
            for (int y = y1 + 1; y < y2; y++)
            {
                double length = noise2[x, y] - noise1[x, y];
                int pointIndex = y - y1;
                double offset = (pointIndex / (double)pointCount) * length;
                if (offset < 0)
                    noise1[x, y] = noise2[x, y] - offset;
                else
                    noise1[x, y] += offset;
            }
        }

        return noise1;
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