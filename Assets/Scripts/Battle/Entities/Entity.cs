using Battle.Entities.Stats;
using System.Collections.Generic;

namespace Battle.Entities
{
    public class Entity
    {

        public string Name { get; }

        public EntityStats EntityStats { get; set; }
        public List<Skill> Skills { get; }

        public Entity(string name, EntityStats stats, List<Skill> skills)
        {
            Name = name;
            EntityStats = stats;
        }

        public override string ToString()
        {
            return $"{Name}: {EntityStats.ToString()}";
        }
    }
}
