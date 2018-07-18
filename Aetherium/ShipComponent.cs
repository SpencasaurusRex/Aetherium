namespace Aetherium
{
    public abstract class ShipComponent
    {
        public abstract bool ComputePowerConsumption(out float min, out float max);

        /// <summary>
        /// Send power to the component.
        /// </summary>
        /// <param name="powerAvailable">The amount of power sent to the component</param>
        /// <returns>The amount of power consumed by the component, can be negative if it produces power.</returns>
        public abstract float Charge(float powerAvailable);

        public abstract void Update();
    }
}