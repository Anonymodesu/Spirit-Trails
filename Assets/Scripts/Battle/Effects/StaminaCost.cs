using System;
using Battle.Entities;
using Battle.Entities.Stats;

namespace Battle.Effects {
    public class StaminaCost: IEffect {

        public Entity Entity { get; }
        public int Cost { get; }

        public StaminaCost(Entity entity, int staminaCost) {
            if(staminaCost < 0) {
                throw new ArgumentException($"Stamina cost {staminaCost} should be positive.");
            }

            this.Entity = entity;
            this.Cost = staminaCost;
        }

        public void Activate() {
            EntityStats currentStats = Entity.EntityStats;
            if(currentStats.Stamina < Cost) {
                throw new InvalidOperationException($"Have {currentStats.Stamina}, but {Cost} required.");
            }

            Entity.EntityStats = currentStats.AdjustStamina(-Cost);
        }
    }
}
