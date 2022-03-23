using Battle.Entities;
using Battle.UI;

namespace Battle.Skills
{
    public abstract class NoTargetSkill : Skill {
                
        public NoTargetSkill(ICondition condition) : base(condition) {
            
        }

        public override ISkillSelectConfig InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }

        public abstract IEffect Build(Entity source);
    }
}
