using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
namespace Battle.Skills
{
    class NoAction : NoTargetSkill {

        public override string Name { get => "No Action"; }
                
        public NoAction() : base(new NullCondition()) {
        }

        public IEffect Build(Entity source, Entity target) =>
            new EmptyEffect();

    }
}
