namespace Aetherium
{
    public class Hull
    {
        public float MaxHealth { get; private set; }

        public float currentHealth { get; private set; }

        public float CurrentHealth
        {
            get
            {
                if (currentHealth < 0)
                {
                    currentHealth = 0;
                }

                return currentHealth;
            }

            private set
            {
                currentHealth = value;
            }
        }

        public bool IsDead
        {
            get { return currentHealth <= 0; }
        }

        public Hull(float maxHealth)
        {
            CurrentHealth = MaxHealth = maxHealth;
        }

        public bool Damage(float amount, DamageType type)
        {
            // TODO: Allow damaging different parts of the hull (armored/non-armored, individual Components)
            // TODO: Take into account type of damage
            CurrentHealth -= amount;
            return IsDead;
        }
    }
}