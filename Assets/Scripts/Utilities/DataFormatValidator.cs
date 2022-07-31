using System;

namespace Utilities
{
    public class DataFormatValidator
    {
        public const string _timeFormat = "mm':'ss";

        public static string SecondsToTimeFormat(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(_timeFormat);
        }
    }
}