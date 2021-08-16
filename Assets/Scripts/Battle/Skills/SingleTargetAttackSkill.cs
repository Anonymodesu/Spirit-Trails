using Battle.Entities;
using Battle.Skills.Conditions;

namespace Battle.Skills
{
    abstract class SingleTargetAttackSkill : Skill {
        public int BasePower { get; }
        public Entity Source { get; }
        public Entity Target { get; }

        public SingleTargetAttackSkill(Entity source, Entity target, int basePower, ICondition condition): base(condition) {
            BasePower = basePower;
            Source = source;
            Target = target;
        }
    }
}
