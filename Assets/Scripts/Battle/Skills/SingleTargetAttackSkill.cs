using Battle.Entities;

namespace Battle.Skills
{
    abstract class SingleTargetAttackSkill : Skill {
        public int BasePower { get; }

        public SingleTargetAttackSkill(int basePower, ICondition condition): base(condition) {
            BasePower = basePower;
        }

        public abstract IEffect Build(Entity source, Entity target);
    }
}
