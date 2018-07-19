using System;
using UnityEngine;

namespace Aetherium.Weapons
{
    public class Laser : Weapon
    {
        readonly WeaponSlotType slotType;

        // Guideline for shot cost: max power consumption / firing rate
        readonly float powerUsagePerShot;

        // Count upwards as time passes
        float currentWaitTime;

        public Laser(WeaponSlotType slotType, float maxFireRate, MinMax powerUsage, float powerUsagePerShot,
            float powerBufferSize) : base(slotType, maxFireRate, powerUsage, powerBufferSize)
        {
            this.slotType = slotType;
            this.powerUsagePerShot = powerUsagePerShot;
        }

        public override void Update()
        {
            const float DELTA_TIME = 0.01f;
            currentWaitTime += DELTA_TIME;
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