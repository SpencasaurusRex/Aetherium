using UnityEngine;

namespace Aetherium
{
    public abstract class ShipComponent
    {
        public readonly PowerBuffer Buffer;
        public readonly MinMax PowerConsumption;

        protected ShipComponent(MinMax powerConsumption, float powerBufferSize)
        {
            PowerConsumption = powerConsumption;
            Buffer = new PowerBuffer(powerBufferSize);
        }

        /// <summary>
        /// Send power to the component.
        /// </summary>
        /// <param name="powerAvailable">The amount of power sent to the component</param>
        /// <returns>The amount of power consumed by the component, can be negative if it produces power.</returns>
        public virtual float Charge(float powerAvailable)
        {
            return Buffer.Charge(powerAvailable);
        }

        public abstract void Update();
    }
}