using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
using Battle.UI;

namespace Battle.Skills
{
    class MagicBolt : SingleTargetSkill {

        private const int basePower = 5;
        private const int manaCost = 11;

        public override string Name { get => "Magic Bolt"; }

        public MagicBolt()
            : base(new ManaCondition(manaCost)) {
        }


        public override IEffect Build(Entity source, AbstractEntity target) =>
            new ManaCost(source, manaCost)
            .Next(new MagicDamage(source, target, basePower));
    }
}
