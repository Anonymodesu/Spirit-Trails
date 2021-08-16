using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
namespace Battle.Skills
{
    class TripleStrike : SingleTargetAttackSkill {

        private const int basePower = 5;
        private const int staminaConsumption = 5;

        public override string Name { get => "Triple Strike"; }

        public TripleStrike(Entity source, Entity target)
            : base(source, target, basePower, new StaminaCondition(staminaConsumption)) {
        }

        public override IEffect Build() =>
            new StaminaCost(Source, staminaConsumption)
            .Next(new PhysicalDamage(Source, Target, BasePower))
            .Next(new PhysicalDamage(Source, Target, BasePower))
            .Next(new PhysicalDamage(Source, Target, BasePower));
    }
}
