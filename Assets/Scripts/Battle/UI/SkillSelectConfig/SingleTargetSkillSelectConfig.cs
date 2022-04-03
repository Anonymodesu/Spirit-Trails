using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
public class SingleTargetSkillSelectConfig : TargettedSkillSelectConfig<PhysicalEntity> {

    public SingleTargetSkillSelectConfig(SingleTargetAttackSkill skill, PhysicalEntity source, PhysicalEntity target) {
        this.Source = source;
        this.Skill = skill;
        this.Target = target;
    }

    public override string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.EntityData.Name}";
    }

    public override IEffect Build() {
        return ((SingleTargetAttackSkill) Skill).Build(this.Source.EntityData, this.Target.EntityData);
    }
}
}