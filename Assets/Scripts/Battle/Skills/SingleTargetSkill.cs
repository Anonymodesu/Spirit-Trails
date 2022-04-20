using Battle.Entities;
using Battle.UI;

namespace Battle.Skills
{
    public abstract class SingleTargetSkill : Skill {

        public SingleTargetSkill(ICondition condition): base(condition) {
        }

        public abstract IEffect Build(Entity source, Entity target);

        public override ISkillSelectConfig InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }
    }
}
