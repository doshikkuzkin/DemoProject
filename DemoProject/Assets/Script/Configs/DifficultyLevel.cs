using System;

namespace Script.Configs
{
    [Serializable]
    public class DifficultyLevel
    {
        public int linesToComplete;
        public float secondsBetweenBlockMove;
        public int scorePointsForLine;
    }
}