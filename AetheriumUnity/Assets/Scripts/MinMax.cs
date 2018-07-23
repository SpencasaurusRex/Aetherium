using System;

namespace Aetherium
{
    [Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
