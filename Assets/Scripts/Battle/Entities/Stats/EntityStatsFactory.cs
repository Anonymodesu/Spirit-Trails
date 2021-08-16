namespace Battle.Entities.Stats
{
    class EntityStatsFactory
    {
        private int maxHealth;
        private int currentHealth;
        private int stamina;
        private int maxMana;
        private int currentMana;
        private int attack;
        private int defence;
        private int magicPotency;
        private int magicResistance;

        public EntityStatsFactory(EntityStats fields) {
            this.maxHealth = fields.MaxHealth;
            this.currentHealth = fields.CurrentHealth;
            this.stamina = fields.Stamina;
            this.maxMana = fields.MaxMana;
            this.currentMana = fields.CurrentMana;
            this.attack = fields.Attack;
            this.defence = fields.Defence;
            this.magicPotency = fields.MagicPotency;
            this.magicResistance = fields.MagicResistance;
        }

        public EntityStatsFactory SetCurrentHealth(int currentHealth) {
            this.currentHealth = currentHealth;
            return this;
        }

        public EntityStatsFactory SetCurrentMana(int currentMana) {
            this.currentMana = currentMana;
            return this;
        }

        public EntityStatsFactory SetStamina(int stamina) {
            this.stamina = stamina;
            return this;
        }

        public EntityStatsFactory SetAttack(int attack) {
            this.attack = attack;
            return this;
        }

        public EntityStatsFactory SetDefence(int defence) {
            this.defence = defence;
            return this;
        }

        public EntityStatsFactory SetMagicPotency(int magicPotency) {
            this.magicPotency = magicPotency;
            return this;
        }

        public EntityStatsFactory SetMagicResistance(int magicResistance) {
            this.magicResistance = magicResistance;
            return this;
        }
        public EntityStats Build() => new EntityStats(
            maxHealth,
            currentHealth,
            stamina,
            maxMana,
            currentMana,
            attack,
            defence,
            magicPotency,
            magicResistance
        );
    }
}
