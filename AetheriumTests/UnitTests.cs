using System.Diagnostics;
using Aetherium;
using Aetherium.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace AetheriumTests
{
    [TestClass]
    public class UnitTests
    {
        const float EPSILON = 0.00000001f;

        [TestMethod]
        public void TestChargingAmounts()
        {
            Hull h = new Hull(100);
            var largeSlot = new WeaponSlot(WeaponSlotType.Large);
            var mediumSlot = new WeaponSlot(WeaponSlotType.Medium);
            var smallSlot = new WeaponSlot(WeaponSlotType.Small);

            var amazingLaser = new Laser(WeaponSlotType.Large, 5, new MinMax(5, 7), 35, 50);
            var goodLaser = new Laser(WeaponSlotType.Medium, 2, new MinMax(2, 3), 3, 8);
            var crappyLaser = new Laser(WeaponSlotType.Small, 1, new MinMax(1, 1), 1, 1);

            largeSlot.Assign(amazingLaser, out var _);
            mediumSlot.Assign(goodLaser, out var _);
            smallSlot.Assign(crappyLaser, out var _);

            WeaponSlot[] slots = new WeaponSlot[] { largeSlot, mediumSlot, smallSlot };
            Ship s = new Ship(h, slots);

            var weaponsSystem = s.Systems[0];
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 7;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 8;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 11;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 13;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.Active = true;

            weaponsSystem.PowerLevel = 0;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 7;
            Assert.AreEqual(0, weaponsSystem.ChargeComponents());

            weaponsSystem.PowerLevel = 8;
            Assert.AreEqual(8 * Time.fixedDeltaTime, weaponsSystem.ChargeComponents(), EPSILON);

            weaponsSystem.PowerLevel = 11;
            Assert.AreEqual(11 * Time.fixedDeltaTime, weaponsSystem.ChargeComponents(), EPSILON);

            weaponsSystem.PowerLevel = 13;
            Assert.AreEqual(11 * Time.fixedDeltaTime, weaponsSystem.ChargeComponents(), EPSILON);
        }

        [TestMethod]
        public void TestChargingInternalBuffers()
        {
            float bufferSize = 2;
            var smallSlot = new WeaponSlot(WeaponSlotType.Small);
            var crappyLaser = new Laser(WeaponSlotType.Small, 1, new MinMax(1, 1), 1, 2);
            smallSlot.Assign(crappyLaser, out var _);

            WeaponSlot[] slots = { smallSlot };
            Ship s = new Ship(new Hull(100), slots);

            var weapons = s.Systems[0];
            weapons.Active = true;
            weapons.PowerLevel = 1;
            float expectedChargeAmount = 0;
            for (int i = 1; i < bufferSize / Time.fixedDeltaTime; i++)
            {
                float prevChargeAmount = expectedChargeAmount;
                expectedChargeAmount = Mathf.Min(Time.fixedDeltaTime * i, bufferSize);
                float powerUsed = expectedChargeAmount - prevChargeAmount;

                Assert.AreEqual(powerUsed, weapons.ChargeComponents(), 0.00001f);
                Assert.AreEqual(expectedChargeAmount, crappyLaser.Buffer.PowerLevel, 0.00001f);
            }
        }

        [TestMethod]
        public void TestWeaponUse()
        {
            float bufferSize = 2;
            var smallSlot = new WeaponSlot(WeaponSlotType.Small);
            var crappyLaser = new Laser(WeaponSlotType.Small, .02f, new MinMax(1, 1), .5f, 2);
            smallSlot.Assign(crappyLaser, out var _);

            WeaponSlot[] slots = new WeaponSlot[] { smallSlot };
            Ship s = new Ship(new Hull(100), slots);

            var weapons = s.Systems[0];
            weapons.Active = true;
            weapons.PowerLevel = 1;
            crappyLaser.Buffer.Charge(2);
            crappyLaser.Update();
            crappyLaser.Update();

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(true, crappyLaser.CanFire);
                crappyLaser.Fire();
                Assert.AreEqual(false, crappyLaser.CanFire);
                crappyLaser.Update();
                Assert.AreEqual(false, crappyLaser.CanFire);
                crappyLaser.Update();
            }

            // Out of energy
            Assert.AreEqual(false, crappyLaser.CanFire);
        }

        [TestMethod]
        public void TestEngineUse()
        {
            
        }
    }
}
