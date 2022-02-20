using System;
namespace Battle.Entities.Stats
{
    public class EntityStats
    {
        public int MaxHealth { get; }
        public int CurrentHealth { get; }
        public int Stamina { get; }
        public int MaxMana { get; }
        public int CurrentMana { get; }
        public int Attack { get; }
        public int Defence { get; }
        public int MagicPotency { get; }
        public int MagicResistance { get; }
        public int Speed { get; }

        public EntityStats(int maxHP, int currentHP, int stamina, int maxMana, int currentMana, int attack, int defence, int magicPotency, int magicResistance, int speed)
        {
            if(currentHP > maxHP || currentHP < 0) {
                throw new ArgumentException($"Invalid values for HP: {currentHP}/{maxHP}");
            }
            if(stamina > currentHP || stamina < 0) {
                throw new ArgumentException($"Invalid values for Stamina: {stamina}/{currentHP}");
            }
            if(currentMana > maxMana || currentMana < 0) {
                throw new ArgumentException($"Invalid values for Mana: {currentMana}/{maxMana}");
            }
            if(attack < 0) {
                throw new ArgumentException($"Invalid values for attack: {attack}");
            }
            if(defence < 0) {
                throw new ArgumentException($"Invalid values for defence: {defence}");
            }
            if(magicPotency < 0) {
                throw new ArgumentException($"Invalid values for magicPotency: {magicPotency}");
            }
            if(magicResistance < 0) {
                throw new ArgumentException($"Invalid values for magicResistance: {magicResistance}");
            }
            if(speed < 0) {
                throw new ArgumentException($"Invalid values for magicResistance: {magicResistance}");
            }

            MaxHealth = maxHP;
            CurrentHealth = currentHP;
            Stamina = stamina;
            MaxMana = maxMana;
            CurrentMana = currentMana;
            Attack = attack;
            Defence = defence;
            MagicPotency = magicPotency;
            MagicResistance = magicResistance;
            Speed = speed;
        }

        public override string ToString()
        {
            return $"Health:{CurrentHealth}/{MaxHealth} Stamina:{Stamina}/{CurrentHealth} Mana:{CurrentMana}/{MaxMana} " +
            $"Attack:{Attack} Defence:{Defence} MagicPotency:{MagicPotency} MagicResistance:{MagicResistance} Speed: {Speed}";
        }
    }
}
