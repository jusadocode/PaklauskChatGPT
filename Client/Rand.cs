namespace Client;

public static class Rand
{
    private static readonly Random _random = new();

    public static int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    public static double NextDouble()
    {
        return _random.NextDouble();
    }
}