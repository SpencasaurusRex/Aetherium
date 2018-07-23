namespace UnityEngine
{
    public static class Time
    {
        public static float deltaTime;
        public static float fixedDeltaTime;

        static Time()
        {
            deltaTime = 0.01f;
            fixedDeltaTime = 0.01f;
        }
    }
}
