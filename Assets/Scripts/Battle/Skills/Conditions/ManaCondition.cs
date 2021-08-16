using Battle.Entities;

namespace Battle.Skills.Conditions
{
    class ManaCondition: ICondition {

        private int mana;
        public ManaCondition(int mana) {
            this.mana = mana;
        }
        public bool IsSatisfied(Entity source) =>
            source.EntityStats.CurrentMana >= mana;
    }
}
