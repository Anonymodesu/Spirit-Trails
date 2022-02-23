using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
class NoTargetSkillSelectConfig : AbstractSkillSelectConfig {

    public NoTargetSkillSelectConfig(NoTargetSkill skill, PhysicalEntity source) {
        this.Skill = skill;
        this.Source = source;
    }

    public override string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name}";
    }

    public override IEffect Build() {
        return ((NoTargetSkill) Skill).Build(this.Source.EntityData);
    }
}
}