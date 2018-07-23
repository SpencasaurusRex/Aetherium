using UnityEngine;

namespace Aetherium
{
    [CreateAssetMenu]
    public class ShipSystemInitializer : ScriptableObject
    {
        public ScriptableObject[] ComponentInitializers;

        public ShipSystem CreateShipSystem()
        {
            ShipSystem system = new ShipSystem();
            foreach (var componentInitializers in ComponentInitializers)
            {
                var shipComponent = (componentInitializers as ShipComponentInitializer)?.CreateShipComponent();
                system.AddComponent(shipComponent);
            }

            return system;
        }
    }
}