using System.Collections.Generic;
using UnityEngine;

namespace Aetherium
{
    public class Ship : MonoBehaviour
    {
        public ShipSystemInitializer[] SystemInitializers;
        public List<ShipSystem> Systems { get; private set; }

        void Awake()
        {
            Systems = new List<ShipSystem>();
            foreach (var systemInitializer in SystemInitializers)
            {
                Systems.Add(systemInitializer.CreateShipSystem());
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
