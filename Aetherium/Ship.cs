using System.Collections.Generic;

namespace Aetherium
{

    public class Ship
    {
        public readonly Hull Hull;
        public readonly WeaponSlot[] WeaponSlots;
        public readonly List<ShipSystem> Systems;

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
    }
}
