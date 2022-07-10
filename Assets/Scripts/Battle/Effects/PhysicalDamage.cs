using Battle.Entities;
using Battle.Entities.Stats;
using Battle.UI;

namespace Battle.Effects {
    public class PhysicalDamage: Damage {

        public PhysicalDamage(Entity source, AbstractEntity target, int basePower): base(source, target, basePower) { }

        public override void Activate() {
            Target.Interact(this);
        }

        public override int CalculateDamage(EntityStats targetFields) => 
            Source.EntityStats.Attack - targetFields.Defence + BasePower;
    }
}
