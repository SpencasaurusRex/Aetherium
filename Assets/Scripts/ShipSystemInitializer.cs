using UnityEngine;

namespace Aetherium
{
    [CreateAssetMenu]
    public class ShipSystemInitializer : ScriptableObject
    {
        public ShipComponentInitializer[] ComponentInitializers;

        public ShipSystem CreateShipSystem()
        {
            ShipSystem system = new ShipSystem();
            foreach (var componentInitializers in ComponentInitializers)
            {
                system.AddComponent(componentInitializers.CreateShipComponent());
            }

            return system;
        }
    }
}