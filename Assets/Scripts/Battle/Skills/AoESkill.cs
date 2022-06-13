using Battle.Entities;
using Battle.UI;
using System.Collections.Generic;

namespace Battle.Skills
{
    public abstract class AoESkill : Skill {
        public int BasePower { get; }

        public AoESkill(int basePower, ICondition condition): base(condition) {
            BasePower = basePower;
        }

        public abstract int Radius { get; }

        public abstract IEffect Build(Entity source, IEnumerable<Entity> target);

        public override ISkillSelectConfig InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }

    }
}
