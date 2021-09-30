using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
namespace Battle.Skills
{
    class TripleStrike : SingleTargetAttackSkill {

        private const int basePower = 5;
        private const int staminaConsumption = 5;

        public override string Name { get => "Triple Strike"; }

        public TripleStrike()
            : base(basePower, new StaminaCondition(staminaConsumption)) {
        }

        public override IEffect Build(Entity source, Entity target) =>
            new StaminaCost(source, staminaConsumption)
            .Next(new PhysicalDamage(source, target, BasePower))
            .Next(new PhysicalDamage(source, target, BasePower))
            .Next(new PhysicalDamage(source, target, BasePower));
    }
}
