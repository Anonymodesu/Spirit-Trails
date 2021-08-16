namespace Battle.Entities {
    
    public abstract class Skill {

        public abstract string Name { get; }

        private ICondition condition;

        public Skill(ICondition condition) {
            this.condition = condition;
        }

        public abstract IEffect Build();

        public bool IsUseable(Entity source) => condition.IsSatisfied(source);
    }
}

