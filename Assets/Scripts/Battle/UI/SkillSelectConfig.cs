using Battle.Entities;

namespace Battle.UI
{
    
class SkillSelectConfig {
    public Entity Source { get; set; }
    public Entity Target { get; set; }
    public Skill Skill { get; set; }

    public override string ToString() {
        return $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }
}
}