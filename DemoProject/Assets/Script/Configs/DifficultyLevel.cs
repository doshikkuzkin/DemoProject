using System;

namespace Script.Installers
{
    [Serializable]
    public class DifficultyLevel
    {
        public int linesToComplete;
        public float secondsBetweenBlockMove;
        public int scorePointsForLine;
    }
}