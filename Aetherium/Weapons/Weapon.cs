namespace Aetherium
{
    // TODO: Also make weapons physical objects in the world
    public abstract class Weapon : ShipComponent
    {
        public abstract WeaponSlotType SlotType { get; }
        public abstract bool CanFire { get; }
        public abstract void Fire();
    }
}