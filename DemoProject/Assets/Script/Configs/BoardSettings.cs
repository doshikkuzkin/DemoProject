using System;

namespace Script.Installers
{
    [Serializable]
    public class BoardSettings
    {
        public int boardRightBoundary;
        public int boardLeftBoundary;
        public int boardBottomBoundary;
        public int boardTopBoundary;
    }
}