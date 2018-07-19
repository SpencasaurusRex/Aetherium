using Aetherium;
using UnityEngine;

namespace Aetherium
{
    public abstract class Engine : ShipComponent
    {
        public abstract bool CanFire { get; }
        /// <summary>
        /// Fire the engines with the energy available in the power buffer.
        /// </summary>
        /// <returns>How much force the engines produced.</returns>
        public abstract void Fire(out float force, out float maneuverability);
        public readonly float ForcePerEnergy;
        public readonly float ManeuverabilityPerEnergy;
        public readonly float MaxEnergyUse;

        protected Engine(float forcePerEnergy, float maneuverabilityPerEnergy, float maxEnergyUse, MinMax powerConsumption, float powerBufferSize)
            : base(powerConsumption, powerBufferSize)
        {
            ForcePerEnergy = forcePerEnergy;
            ManeuverabilityPerEnergy = maneuverabilityPerEnergy;
            MaxEnergyUse = maxEnergyUse;
        }

    }
}