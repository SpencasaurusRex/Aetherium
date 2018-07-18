using System.Collections.Generic;

namespace Aetherium
{
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

        public bool Active => powerLevel >= MinPowerConsumption;

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
        /// Charge all of the Components in the system, given the input energy.
        /// </summary>
        /// <returns>The amount of energy used</returns>
        public float ChargeComponents()
        {
            if (!Active) return 0;

            // Parametric variable from 0 - 1. 0 for min power, 1 for max power.
            float productionRatio = (PowerLevel - MinPowerConsumption) / (MaxPowerConsumption - MinPowerConsumption);
            float inputPower = PowerLevel;

            foreach (var component in Components)
            {
                component.ComputePowerConsumption(out float componentMin, out float componentMax);
                float powerToComponent = productionRatio * (componentMax - componentMin) + componentMin;
                inputPower -= component.Charge(powerToComponent);
            }

            // If for some reason there is leftover power, we don't want to count it as used.
            return PowerLevel - inputPower;
        }
    }
}