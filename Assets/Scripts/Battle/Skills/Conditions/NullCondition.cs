using Battle.Entities;

namespace Battle.Skills.Conditions
{
    class NullCondition: ICondition {
        public bool IsSatisfied(Entity source) => true;
    }
}
