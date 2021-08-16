using Battle.Entities;

namespace Battle.Skills.Conditions
{
    class StaminaCondition: ICondition {

        private int stamina;
        public StaminaCondition(int stamina) {
            this.stamina = stamina;
        }
        public bool IsSatisfied(Entity source) =>
            source.EntityStats.Stamina >= stamina;
    }
}
