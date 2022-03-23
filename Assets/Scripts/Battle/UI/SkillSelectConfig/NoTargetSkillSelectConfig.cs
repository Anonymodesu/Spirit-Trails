using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
public class NoTargetSkillSelectConfig : ISkillSelectConfig {
    public PhysicalEntity Source { get; }
    public Skill Skill { get; }

    public NoTargetSkillSelectConfig(NoTargetSkill skill, PhysicalEntity source) {
        this.Skill = skill;
        this.Source = source;
    }

    public string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name}";
    }

    public IEffect Build() {
        return ((NoTargetSkill) Skill).Build(this.Source.EntityData);
    }
}
}