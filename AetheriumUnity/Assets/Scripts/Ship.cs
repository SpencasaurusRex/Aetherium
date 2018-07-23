using System.Collections.Generic;
using UnityEngine;

namespace Aetherium
{
    public class Ship : MonoBehaviour
    {
        public ShipInitializer Initializer;
        public List<ShipSystem> Systems;
        public PowerBuffer Battery { get; private set; }

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            Battery = new PowerBuffer(Initializer.PowerBufferSize, Initializer.PowerBufferSize);
            Systems = Initializer.CreateSystems();
        }

        void FixedUpdate()
        {
            foreach (var system in Systems)
            {
                system.ChargeComponents();
            }
        }
    }
}