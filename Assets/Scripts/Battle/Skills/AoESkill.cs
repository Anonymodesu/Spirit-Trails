using Battle.Entities;
using Battle.UI;
using System.Collections.Generic;

namespace Battle.Skills
{
    public abstract class AoESkill : Skill {

        public AoESkill(ICondition condition): base(condition) {
        }

        public abstract int Radius { get; }

        public abstract IEffect Build(Entity source, IEnumerable<AbstractEntity> targets);

        public override ISkillSelectConfig InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }

    }
}
