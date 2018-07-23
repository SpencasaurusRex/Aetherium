using System.Collections.Generic;
using UnityEngine;

namespace Aetherium
{
    [CreateAssetMenu]
    public class ShipInitializer : ScriptableObject
    {
        public ShipSystemInitializer[] SystemIntializers;
        public float PowerBufferSize;

        public List<ShipSystem> CreateSystems()
        {
            List<ShipSystem> systems = new List<ShipSystem>();
            foreach (var systemInit in SystemIntializers)
            {
                systems.Add(systemInit.CreateShipSystem());
            }

            return systems;
        }
    }
}
