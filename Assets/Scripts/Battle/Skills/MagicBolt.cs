using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;

namespace Battle.Skills
{
    class MagicBolt : SingleTargetAttackSkill {

        private const int basePower = 5;
        private const int manaCost = 11;

        public override string Name { get => "Magic Bolt"; }

        public MagicBolt(Entity source, Entity target)
            : base(source, target, basePower, new ManaCondition(manaCost)) {
        }


        public override IEffect Build() =>
            new ManaCost(Source, manaCost)
            .Next(new MagicDamage(Source, Target, BasePower));
    }
}
