using UnityEngine;

namespace Aetherium.Weapons
{
    // TODO: Also make weapons physical objects in the world
    public abstract class Weapon : ShipComponent
    {
        public WeaponSlotType SlotType { get; private set; }
        public abstract bool CanFire { get; }
        public abstract void Fire();
        public readonly float MaxFireWaitTime;

        protected Weapon(WeaponSlotType slotType, float maxFireWaitTime, MinMax powerConsumption, float powerBufferSize)
            : base(powerConsumption, powerBufferSize)
        {
            SlotType = slotType;
            MaxFireWaitTime = maxFireWaitTime;
        }
    }
}