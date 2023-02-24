namespace TariffComparison.Helper
{
    public static class CommonHelper
    {
        public static bool IsNullOrEmpty<T>(this List<T> source)
        {
            return source == null || !source.Any();
        }

        public static decimal ConvertToDollar(decimal coin)
        {
            return coin / 100;
        }
    }
}
