using System;

namespace Aetherium.Weapons
{
    public class Laser : Weapon
    {
        readonly float minPowerUsage;
        readonly float maxPowerUsage;
        readonly float hardwareMinWaitTime;
        readonly float shotPowerUsage;
        readonly float powerBufferSize;

        // Count upwards as time passes
        float powerBuffer;
        float hardwareWaitTime;

        public Laser(WeaponSlotType slotType, float minPowerUsage, float maxPowerUsage, float hardwareMinWaitTime,
            float shotPowerUsage, float powerBufferSize)
        {
            SlotType = slotType;
            this.minPowerUsage = minPowerUsage;
            this.maxPowerUsage = maxPowerUsage;
            this.hardwareMinWaitTime = hardwareMinWaitTime;
            this.shotPowerUsage = shotPowerUsage;
            this.powerBufferSize = powerBufferSize;
        }

        public override bool ComputePowerConsumption(out float min, out float max)
        {
            min = minPowerUsage;
            max = maxPowerUsage;
            return true;
        }

        public override float Charge(float power)
        {
            float roomForPower = powerBufferSize - powerBuffer;
            float powerUsed = Math.Min(power, roomForPower);
            powerBuffer += powerUsed;
            return powerUsed;
        }

        public override void Update()
        {
            const float DELTA_TIME = 0.01f;
            hardwareWaitTime += DELTA_TIME;
        }

        public override WeaponSlotType SlotType { get; }

        public override bool CanFire =>
            hardwareWaitTime >= hardwareMinWaitTime && powerBuffer >= shotPowerUsage;

        public override void Fire()
        {
            if (!CanFire) return;

            hardwareWaitTime = 0;
            powerBuffer -= shotPowerUsage;
        }
    }
}