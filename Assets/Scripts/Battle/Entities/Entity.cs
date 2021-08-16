using Battle.Effects;
using Battle.Entities.Stats;


namespace Battle.Entities
{
    public class Entity
    {

        public string Name { get; }

        public EntityStats EntityStats { get; set; }

        public Entity(string name, EntityStats stats)
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
