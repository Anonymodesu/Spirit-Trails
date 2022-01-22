using System.Collections;

namespace Battle.Entities {
    
    public abstract class Skill {

        public abstract string Name { get; }

        private ICondition condition;

        public Skill(ICondition condition) {
            this.condition = condition;
        }

        public bool IsUseable(Entity source) => condition.IsSatisfied(source);
        public abstract IEnumerator InitiateSkillTargeting(ISkillTargetMode skillTargetMode);

    }
}

