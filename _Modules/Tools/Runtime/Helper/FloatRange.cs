using System;

namespace DivineSkies.Tools.Helper
{
    [Serializable]
    public class FloatRange
    {
        public float Min;
        public float Max;

        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandomValue() => UnityEngine.Random.Range(Min, Max);
    }
}