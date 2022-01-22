using Battle.Entities;
using System.Collections;

namespace Battle.Skills
{
    public abstract class SingleTargetAttackSkill : Skill {
        public int BasePower { get; }

        public SingleTargetAttackSkill(int basePower, ICondition condition): base(condition) {
            BasePower = basePower;
        }

        public abstract IEffect Build(Entity source, Entity target);

        public override IEnumerator InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }
    }
}
