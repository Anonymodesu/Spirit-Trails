using Battle.Entities;
using Battle.Entities.Stats;
using Battle.UI;

namespace Battle.Effects {
    public abstract class Damage: IEffect {

        public Entity Source { get; }
        public AbstractEntity Target { get; }
        public int BasePower { get; }

        public Damage(Entity source, AbstractEntity target, int basePower) {
            Source = source;
            Target = target;
            BasePower = basePower;
        }

        public abstract void Activate();


        public abstract int CalculateDamage(EntityStats targetFields);
    }
}
