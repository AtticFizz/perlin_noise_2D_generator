namespace NoiseGeneration;

internal static class ExtensionMethods
{
    public static double Remap(this double value, double from1, double to1, double from2, double to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static void Shuffle<T>(this List<T> list, Random randomNumberGenerator)
    {
        for (int i = list.Count - 1; i > 1; i--)
        {
            int index = randomNumberGenerator.Next(i + 1);
            T value = list[index];
            list[index] = list[i];
            list[i] = value;
        }
    }
}
