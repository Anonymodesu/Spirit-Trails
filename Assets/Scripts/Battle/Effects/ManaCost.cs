using System;
using Battle.Entities;
using Battle.Entities.Stats;

namespace Battle.Effects {
    public class ManaCost: IEffect {

        public Entity Entity { get; }
        public int Cost { get; }

        public ManaCost(Entity entity, int manaCost) {
            if(manaCost < 0) {
                throw new ArgumentException($"Mana cost {manaCost} should be positive.");
            }

            this.Entity = entity;
            this.Cost = manaCost;
        }

        public void Activate() {
            EntityStats currentStats = Entity.EntityStats;
            if(currentStats.CurrentMana < Cost) {
                throw new InvalidOperationException($"Have {currentStats.CurrentMana}, but {Cost} required.");
            }

            Entity.EntityStats = currentStats.AdjustMana(-Cost);
        }
    }
}
