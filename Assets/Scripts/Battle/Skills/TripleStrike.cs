using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
using Battle.UI;
namespace Battle.Skills
{
    class TripleStrike : SingleTargetSkill {

        private const int basePower = 5;
        private const int staminaConsumption = 5;

        public override string Name { get => "Triple Strike"; }

        public TripleStrike()
            : base(new StaminaCondition(staminaConsumption)) {
        }

        public override IEffect Build(Entity source, AbstractEntity target) =>
            new StaminaCost(source, staminaConsumption)
            .Next(new PhysicalDamage(source, target, basePower))
            .Next(new PhysicalDamage(source, target, basePower))
            .Next(new PhysicalDamage(source, target, basePower));
    }
}
