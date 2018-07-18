using System.Diagnostics;
using Aetherium;
using Aetherium.Weapons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AetheriumTests
{
    [TestClass]
    public class UnitTests
    {
        const float EPSILON = 0.00000001f;

        [TestMethod]
        public void Test()
        {
            Hull h = new Hull(100);
            var largeSlot = new WeaponSlot(WeaponSlotType.Large);
            var mediumSlot = new WeaponSlot(WeaponSlotType.Medium);
            var smallSlot = new WeaponSlot(WeaponSlotType.Small);

            var amazingLaser = new Laser(WeaponSlotType.Large, 5, 7, 5, 35, 50);
            var goodLaser = new Laser(WeaponSlotType.Medium, 2, 3, 1, 3, 3);
            var crappyLaser = new Laser(WeaponSlotType.Small, 1, 1, 0.5f, 0.5f, 0.5f);

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
            Assert.AreEqual(.075f, weaponsSystem.ChargeComponents(), EPSILON);

            
        }
    }
}
