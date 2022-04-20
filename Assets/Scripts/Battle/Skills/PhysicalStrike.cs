using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
namespace Battle.Skills
{
    class PhysicalStrike : SingleTargetSkill {

        private const int basePower = 5;
        private const int staminaConsumption = 2;


        public override string Name { get => "Physical Strike"; }
                
        public PhysicalStrike()
            : base(new StaminaCondition(staminaConsumption)) {
        }

        public override IEffect Build(Entity source, Entity target) =>
            new StaminaCost(source, staminaConsumption)
            .Next(new PhysicalDamage(source, target, basePower));

    }
}
