using System.Collections.Generic;
using UnityEngine;

namespace Aetherium
{
    public class Ship : MonoBehaviour
    {
        public ScriptableObject[] SystemInitializers;
        public List<ShipSystem> Systems { get; private set; }

        void Awake()
        {
            Systems = new List<ShipSystem>();
            foreach (var systemInitializer in SystemInitializers)
            {
                var ShipSystem = (systemInitializer as ShipSystemInitializer).CreateShipSystem();
                Systems.Add(ShipSystem);
            }
        }

        public void Update()
        {
            foreach (var system in Systems)
            {
                system.ChargeComponents();
            }
        }
    }
}
