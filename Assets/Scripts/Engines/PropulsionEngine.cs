using System;

namespace Aetherium.Engines
{
    public class PropulsionEngine : Engine
    {
        public PropulsionEngine(float forcePerEnergy, float maneuverabilityPerEnergy, float maxEnergyUse, MinMax powerConsumption, float powerBufferSize)
            : base(forcePerEnergy, maneuverabilityPerEnergy, maxEnergyUse, powerConsumption, powerBufferSize)
        {
        }

        public override void Update()
        {
               
        }

        public override bool CanFire
        {
            get { return true; }
        }

        public override void Fire(out float force, out float maneuverability)
        {
            float powerSpent = Buffer.Spend(MaxEnergyUse);
            force = powerSpent * ForcePerEnergy;
            maneuverability = powerSpent * ManeuverabilityPerEnergy;
        }
    }
}