using UnityEngine;

namespace Aetherium.Engines
{
    [CreateAssetMenu(menuName = "Engines/Propulsion")]
    public class PropulsionEngineInitializer : ScriptableObject, ShipComponentInitializer
    {
        public float ForcePerEnergy;
        public float ManeuverabilityPerEngine;
        public float MaxEnergyUse;
        public MinMax PowerConsumption;
        public float PowerBufferSize;

        public ShipComponent CreateShipComponent()
        {
            return new PropulsionEngine(ForcePerEnergy, ManeuverabilityPerEngine, MaxEnergyUse, PowerConsumption, PowerBufferSize);
        }
    }

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