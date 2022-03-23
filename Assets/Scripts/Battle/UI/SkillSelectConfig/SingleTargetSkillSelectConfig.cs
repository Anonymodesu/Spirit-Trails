using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
public class SingleTargetSkillSelectConfig : ISkillSelectConfig {
    public PhysicalEntity Source { get; }
    public Skill Skill { get; }

    public PhysicalEntity Target { get; protected set; }

    public SingleTargetSkillSelectConfig(SingleTargetAttackSkill skill, PhysicalEntity source, PhysicalEntity target) {
        this.Skill = skill;
        this.Source = source;
        this.Target = target;
    }

    public string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }

    public IEffect Build() {
        return ((SingleTargetAttackSkill) Skill).Build(this.Source.EntityData, this.Target.EntityData);
    }
}
}