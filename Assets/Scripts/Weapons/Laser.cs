using System;
using UnityEngine;

namespace Aetherium.Weapons
{
    [CreateAssetMenu(menuName = "Weapons/Laser")]
    public class LaserInitializer : ScriptableObject, ShipComponentInitializer
    {
        WeaponSlotType SlotType;
        MinMax PowerUsage;
        float FireWaitTime;
        float PowerUsagePerShot;
        float PowerBufferSize;
        
        public ShipComponent CreateShipComponent()
        {
            return new Laser(SlotType, FireWaitTime, PowerUsage, PowerUsagePerShot, PowerBufferSize);
        }
    }
    
    public class Laser : Weapon
    {
        readonly WeaponSlotType slotType;

        // Guideline for shot cost: max power consumption / firing rate
        readonly float powerUsagePerShot;

        // Count upwards as time passes
        float currentWaitTime;

        public Laser(WeaponSlotType slotType, float maxFireWaitTime, MinMax powerUsage, float powerUsagePerShot,
            float powerBufferSize) : base(slotType, maxFireWaitTime, powerUsage, powerBufferSize)
        {
            this.slotType = slotType;
            this.powerUsagePerShot = powerUsagePerShot;
        }

        public override void Update()
        {
            currentWaitTime += Time.fixedDeltaTime;
        }

        public override bool CanFire
        {
            get { return currentWaitTime >= MaxFireWaitTime && Buffer.PowerLevel >= powerUsagePerShot; }
        }

        public override void Fire()
        {
            if (!CanFire) return;

            currentWaitTime = 0;
            Buffer.Spend(powerUsagePerShot);
        }
    }
}