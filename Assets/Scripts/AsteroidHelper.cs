public static class AsteroidHelper
{
    public static int AsteroidTypesCount = 3;
    public enum Stages
    {
        Small, Medium, Large
    }

    private static readonly int[] Bounties = new[] {100, 50, 20};

    public static int GetBounty(Stages stage)
    {
        return Bounties[(int) stage];
    }
}