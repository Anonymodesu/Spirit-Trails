using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
class SingleTargetSkillSelectConfig : AbstractSkillSelectConfig {
    public PhysicalEntity Target { get; protected set; }

    public SingleTargetSkillSelectConfig(SingleTargetAttackSkill skill, PhysicalEntity source, PhysicalEntity target) {
        this.Skill = skill;
        this.Source = source;
        this.Target = target;
    }

    public override string DisplayText {
        get  => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }

    public override IEffect Build() {
        return ((SingleTargetAttackSkill) Skill).Build(this.Source.EntityData, this.Target.EntityData);
    }
}
}