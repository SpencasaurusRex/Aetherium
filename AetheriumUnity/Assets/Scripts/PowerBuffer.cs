using UnityEngine;

namespace Aetherium
{
    public class PowerBuffer
    {
        public readonly float PowerBufferSize;
        public float PowerLevel { get; private set; }

        public PowerBuffer(float bufferSize, float currentBufferAmount = 0)
        {
            PowerBufferSize = bufferSize;
            PowerLevel = currentBufferAmount;
        }

        /// <summary>
        /// Charge the buffer with the passed amount of power. Up to capacity.
        /// </summary>
        /// <param name="power">The amount of power to try to charge it with.</param>
        /// <returns>The amount of power used to charge.</returns>
        public float Charge(float power)
        {
            float roomLeft = PowerBufferSize - PowerLevel;
            float powerToAdd = Mathf.Min(roomLeft, power);
            PowerLevel += powerToAdd;
            return powerToAdd;
        }

        /// <summary>
        /// Spend power up to the remaining power in the buffer.
        /// </summary>
        /// <param name="power">The power to try to spend.</param>
        /// <returns>The amount of power spent.</returns>
        public float Spend(float power)
        {
            float powerToSpend = Mathf.Min(PowerLevel, power);
            PowerLevel -= powerToSpend;
            return powerToSpend;
        }
    }
}
