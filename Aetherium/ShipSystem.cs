using System.Collections.Generic;

namespace Aetherium
{
    /// <summary>
    /// A container for multiple ship components, and their power needs.
    /// Is responsible for making sure that components are deactivated if less than the minimum amount of power is provided.
    /// Also is responsbile for making sure that components are not provided with more than their maximum amount of power.
    /// </summary>
    public class ShipSystem
    {
        float powerLevel;
        public float PowerLevel
        {
            get => powerLevel;
            set
            {
                if (value < MinPowerConsumption)
                {
                    powerLevel = 0;
                }
                else if (value > MaxPowerConsumption)
                {
                    powerLevel = MaxPowerConsumption;
                }
                else
                {
                    powerLevel = value;
                }
            }
        }

        public bool Active { get; set; }
        public bool EnoughPower => PowerLevel >= MinPowerConsumption;

        public float MinPowerConsumption { get; private set; }

        public float MaxPowerConsumption { get; private set; }

        public readonly List<ShipComponent> Components;

        public ShipSystem(params ShipComponent[] components)
        {
            Components = new List<ShipComponent>();
            Components.AddRange(components);
            ComputePowerConsumption();
        }

        public void AddComponent(ShipComponent component)
        {
            if (Components.Contains(component))
            {
                return;
            }

            Components.Add(component);
            ComputePowerConsumption();
        }

        public void RemoveComponent(ShipComponent component)
        {
            if (!Components.Contains(component))
            {
                return;
            }

            Components.Remove(component);
            ComputePowerConsumption();
        }

        void ComputePowerConsumption()
        {
            float minPower = 0;
            float maxPower = 0;
            foreach (var component in Components)
            {
                component.ComputePowerConsumption(out float min, out float max);
                minPower += min;
                maxPower += max;
            }

            MinPowerConsumption = minPower;
            MaxPowerConsumption = maxPower;

            // Check boundary conditions for input power level
            PowerLevel = PowerLevel;
        }

        /// <summary>
        /// Charge all of the Components in the system, using PowerLevel as the amount of input energy.
        /// </summary>
        /// <returns>The amount of energy used</returns>
        public float ChargeComponents()
        {
            const float DELTA_TIME = .01f;

            if (!Active || !EnoughPower) return 0;
            float inputPower = PowerLevel * DELTA_TIME;

            // Parametric variable from 0 - 1. 0 for min power, 1 for max power.
            float productionRatio = (PowerLevel - MinPowerConsumption) / (MaxPowerConsumption - MinPowerConsumption);
            float powerRemaining = inputPower;
            foreach (var component in Components)
            {
                component.ComputePowerConsumption(out float componentMin, out float componentMax);
                float powerToComponent = productionRatio * (componentMax - componentMin) + componentMin;
                powerToComponent *= DELTA_TIME;
                powerRemaining -= component.Charge(powerToComponent);
            }

            // Don't count leftover power as used.
            return inputPower - powerRemaining;
        }

        /// <summary>
        /// Charge the system with any extra power leftover after the first pass of ChareComponents() across all ShipSystems
        /// PowerLevel should NOT be changed between ChargeComponents() and a call to this method.
        /// </summary>
        /// <param name="power">The amount of extra power to be passed to the system.</param>
        /// <returns></returns>
        public float ChargeComponentsWithLeftoverPower(float power)
        {
            // Even though this if leftover power, if the default power setting isn't enough to activate the component,
            // don't activate it now. We don't want components sporadically coming to life when there is enough leftover power for them.
            // TODO: Do we?
            if (!Active || !EnoughPower) return 0;
            float productionRatio = (PowerLevel - MinPowerConsumption) / (MaxPowerConsumption - MinPowerConsumption);

            foreach (var component in Components)
            {
                component.ComputePowerConsumption(out float componentMin, out float componentMax);
                float powerToComponent = productionRatio * (componentMax - componentMin) + componentMin;
                float roomLeftForPower = componentMax - powerToComponent;
            }
        }
    }
}