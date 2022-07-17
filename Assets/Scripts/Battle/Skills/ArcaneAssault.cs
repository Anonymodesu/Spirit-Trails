using Battle.Entities;
using Battle.Effects;
using Battle.Skills.Conditions;
using Battle.UI;
using System.Collections.Generic;

namespace Battle.Skills
{
    class ArcaneAssault : AoESkill {
        public override int Radius { get => 5; }

        private const int basePower = 8;
        private const int manaCost = 34;

        public override string Name { get => "Arcane Assault"; }

        public ArcaneAssault()
            : base(new ManaCondition(manaCost)) {
        }


        public override IEffect Build(Entity source, IEnumerable<AbstractEntity> targets) {
            IEffect effect = new ManaCost(source, manaCost);
            foreach (AbstractEntity target in targets) {
                effect = effect.Next(new MagicDamage(source, target, basePower));
            }
            return effect;
        }
    }
}
