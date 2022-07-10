using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
public class SingleTargetSkillSelectConfig : TargettedSkillSelectConfig<EntityContainer> {

    public SingleTargetSkillSelectConfig(SingleTargetSkill skill, PhysicalEntity source, EntityContainer target) {
        this.Source = source;
        this.Skill = skill;
        this.Target = target;
    }

    public override string DisplayText {
        get => $"{Source.EntityData.Name} - {Skill.Name} -> {Target.Entity.Name}";
    }

    public override IEffect Build() {
        return ((SingleTargetSkill) Skill).Build(this.Source.EntityData, this.Target.Entity);
    }
}
}