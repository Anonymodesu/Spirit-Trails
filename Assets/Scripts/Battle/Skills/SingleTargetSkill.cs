using Battle.Entities;
using Battle.UI;
using Battle.UI.Entities;
using Battle.Effects;

namespace Battle.Skills
{
    public abstract class SingleTargetSkill : Skill {

        public SingleTargetSkill(ICondition condition): base(condition) {
        }

        public abstract IEffect Build(Entity source, AbstractEntity target);

        public override ISkillSelectConfig InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }
    }
}
