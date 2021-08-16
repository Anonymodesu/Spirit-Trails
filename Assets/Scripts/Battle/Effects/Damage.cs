using Battle.Entities;
using Battle.Entities.Stats;

namespace Battle.Effects {
    abstract class Damage: IEffect {

        public Entity Source { get; }
        public Entity Target { get; }
        public int BasePower { get; }

        public Damage(Entity source, Entity target, int basePower) {
            Source = source;
            Target = target;
            BasePower = basePower;
        }

        public void Activate() {
            EntityStats currentStats = Target.EntityStats;
            int damage = CalculateDamage(Target.EntityStats);
            damage = (damage < 0) ? 0 : damage;
            Target.EntityStats = currentStats.AdjustHealth(-damage);
        }

        public abstract int CalculateDamage(EntityStats targetFields);
    }
}
