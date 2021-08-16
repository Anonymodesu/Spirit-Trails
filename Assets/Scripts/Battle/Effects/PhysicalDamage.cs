using Battle.Entities;
using Battle.Entities.Stats;

namespace Battle.Effects {
    class PhysicalDamage: Damage {

        public PhysicalDamage(Entity source, Entity target, int basePower): base(source, target, basePower) { }

        public override int CalculateDamage(EntityStats targetFields) => 
            Source.EntityStats.Attack - targetFields.Defence + BasePower;
    }
}
