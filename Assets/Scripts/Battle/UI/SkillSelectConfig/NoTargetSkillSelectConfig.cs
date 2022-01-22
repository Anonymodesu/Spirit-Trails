using Battle.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
class NoTargetSkillSelectConfig : AbstractSkillSelectConfig {
    public override string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name}";
    }
}
}