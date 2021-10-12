namespace ZomboTerrain
{
    public sealed class Damage : ITakeDamage
    {
        public int Health { get; private set; }

        public void AddDamage(int damageValue)
        {
            Health -= damageValue;

            if (Health < 0)
                Health = 0;
        }
    }
}