using Battle.Entities;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
class SingleTargetSkillSelectConfig : AbstractSkillSelectConfig {
    public PhysicalEntity Target { get; set; }

    public override string DisplayText {
        get  => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }
}
}