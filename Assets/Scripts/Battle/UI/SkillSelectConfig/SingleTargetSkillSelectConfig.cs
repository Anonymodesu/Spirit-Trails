using Battle.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
class SingleTargetSkillSelectConfig : AbstractSkillSelectConfig {
    public Entity Target { get; set; }

    public override string DisplayText {
        get  => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }
}
}