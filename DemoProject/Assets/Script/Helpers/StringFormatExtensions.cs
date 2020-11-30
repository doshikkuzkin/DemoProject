using UnityEngine;

namespace Script.Helpers
{
    public static class StringFormatExtensions
    {
        public static string FormatScore(this int scoreToFormat)
        {
            return scoreToFormat.ToString("D9");
        }
    }
}