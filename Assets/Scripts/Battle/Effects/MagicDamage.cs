using Battle.Entities;
using Battle.Entities.Stats;
using Battle.UI;

namespace Battle.Effects {
    public class MagicDamage: Damage {

        public MagicDamage(Entity source, AbstractEntity target, int basePower): base(source, target, basePower) { }

        public override void Activate() {
            Target.Interact(this);
        }

        public override int CalculateDamage(EntityStats targetFields) => 
            Source.EntityStats.MagicPotency - targetFields.MagicResistance + BasePower;
    }
}
