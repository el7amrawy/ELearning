namespace ELearning.Extensions
{
    public static class DoubleExtensions
    {
        public static string FormatDuration(this double totalSeconds) {
            if (totalSeconds < 60)
            {
                return $"{Math.Round(totalSeconds, 2)} seconds";
            }
            else if (totalSeconds < 3600)
            {
                double minutes = totalSeconds / 60;
                return $"{Math.Round(minutes, 2)} minutes";
            }
            else
            {
                double hours = totalSeconds / 3600;
                return $"{Math.Round(hours, 2)} hours";
            }
        }
    }
}
