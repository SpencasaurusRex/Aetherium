using Aetherium.Weapons;

namespace Aetherium
{
    public enum WeaponSlotType
    {
        Small,
        Medium,
        Large,
    }

    public class WeaponSlot
    {
        public readonly WeaponSlotType type;
        public Weapon Weapon { get; private set; }

        public WeaponSlot(WeaponSlotType type)
        {
            this.type = type;
        }

        public bool CanAssign(Weapon weapon)
        {
            return weapon.SlotType <= type;
        }

        /// <summary>
        /// Assign a weapon to the slot.
        /// </summary>
        /// <param name="weapon">The weapon to assign.</param>
        /// <param name="oldWeapon">The weapon previously in the slot</param>
        /// <returns>Whether or not the weapon could be assigned.</returns>
        public bool Assign(Weapon weapon, out Weapon oldWeapon)
        {
            if (CanAssign(weapon))
            {
                oldWeapon = Weapon;
                Weapon = weapon;
                return true;
            }
            oldWeapon = null;
            return false;
        }
    }
}
