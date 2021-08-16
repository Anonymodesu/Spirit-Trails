using System;
using UnityEngine;

namespace Battle.Entities.Stats
{
    public static class EntityStatsHelper
    {
        public static EntityStats AdjustHealth(this EntityStats stats, int adjustment) {
            int newHP = Mathf.Clamp(stats.CurrentHealth + adjustment, 0, stats.MaxHealth);
            int newStamina = Mathf.Clamp(stats.Stamina, 0, newHP);
            return new EntityStatsFactory(stats).SetCurrentHealth(newHP).SetStamina(newStamina).Build();
        }

        public static EntityStats AdjustStamina(this EntityStats stats, int adjustment) {
            int newStamina = Mathf.Clamp(stats.Stamina + adjustment, 0, stats.CurrentHealth);
            return new EntityStatsFactory(stats).SetStamina(newStamina).Build();
        }

        public static EntityStats AdjustMana(this EntityStats stats, int adjustment) {
            int newMana = Mathf.Clamp(stats.CurrentMana + adjustment, 0, stats.MaxMana);
            return new EntityStatsFactory(stats).SetCurrentMana(newMana).Build();
        }
    }
}
