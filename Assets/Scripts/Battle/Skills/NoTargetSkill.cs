using Battle.Entities;
using System.Collections;

namespace Battle.Skills
{
    public abstract class NoTargetSkill : Skill {
                
        public NoTargetSkill(ICondition condition) : base(condition) {
            
        }

        public override IEnumerator InitiateSkillTargeting(ISkillTargetMode skillTargetMode) {
            return skillTargetMode.InitiateTargeting(this);
        }
    }
}
