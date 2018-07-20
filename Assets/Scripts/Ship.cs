using System.Collections.Generic;
using UnityEngine;

namespace Aetherium
{
    public class Ship : MonoBehaviour
    {
        public Hull Hull { get; private set; }
        public WeaponSlot[] WeaponSlots { get; private set; }
        public List<ShipSystem> Systems { get; private set; }
        public Engine Engine { get; private set; }

        public Ship()
        {

        }

        public Ship(Hull hull, WeaponSlot[] weaponSlots)
        {
            Hull = hull;
            WeaponSlots = weaponSlots;
            Systems = new List<ShipSystem>();

            // Create one system for all the existing weapons
            ShipSystem weaponsSystem = new ShipSystem();
            foreach (WeaponSlot slot in WeaponSlots)
            {
                if (slot.Weapon == null) continue;
                weaponsSystem.AddComponent(slot.Weapon);
            }
            Systems.Add(weaponsSystem);
        }

        public override void Awake()
        {

        }

        public override void Update()
        {
            foreach (var system in Systems)
            {
                system.ChargeComponents();
            }
        }
    }
}
