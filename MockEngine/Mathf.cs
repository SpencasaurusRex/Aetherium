using System;

namespace UnityEngine
{
    public static class Mathf
    {
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
        
        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }
    }
}