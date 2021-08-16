using Battle.Entities;
using Battle.Entities.Stats;

namespace Battle.Effects {
    class MagicDamage: Damage {

        public MagicDamage(Entity source, Entity target, int basePower): base(source, target, basePower) { }

        public override int CalculateDamage(EntityStats targetFields) => 
            Source.EntityStats.MagicPotency - targetFields.MagicResistance + BasePower;
    }
}
