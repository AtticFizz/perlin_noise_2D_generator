namespace NoiseGenerator2;

internal class Vector2
{
    public double x { get; private set; }
    public double y { get; private set; }

    public double Length { get { return Math.Sqrt(x * x + y * y); } }

    public Vector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 Rotate(double degrees)
    {
        double radians = (degrees * Math.PI) / 180;
        double cosT = Math.Cos(radians);
        double sinT = Math.Sin(radians);
        double xr = x * cosT - y * sinT;
        double yr = x * sinT + y * cosT;
        return new Vector2(xr, yr);
    }

    public Vector2 Normalize()
    {
        if (Length == 0)
            return new Vector2(0, 0);
        return new Vector2(x, y) / Length;
    }

    public double Dot(Vector2 vector)
    {
        return x * vector.x + y * vector.y;
    }

    public static Vector2 operator *(double scaler, Vector2 vector)
    {
        return new Vector2(scaler * vector.x, scaler * vector.y);
    }

    public static Vector2 operator /(Vector2 vector, double scaler)
    {
        return new Vector2(vector.x / scaler, vector.y / scaler);
    }

}