using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Battle.UI.SkillSelectConfig
{
    
public class AoESkillSelectConfig : TargettedSkillSelectConfig<IEnumerable<PhysicalEntity>> {

    public AoESkillSelectConfig(AoESkill skill, PhysicalEntity source, IEnumerable<PhysicalEntity> targets) {
        this.Source = source;
        this.Skill = skill;
        this.Target = targets;
    }

    // Extracts a property from the list of PhysicalEntities
    private List<Prop> GetTargetEntityProperty<Prop>(Func<PhysicalEntity, Prop> propGetter) =>
        Target.Select(propGetter).ToList();

    public override string DisplayText {
        get {
            String targetNames = String.Join(",", GetTargetEntityProperty(e => e.EntityData.Name));
            return $"{Source.EntityData.Name} - {Skill.Name} -> {targetNames}";
        }
    }

    public override IEffect Build() {
        var entityData = GetTargetEntityProperty(e => e.EntityData);
        return ((AoESkill) Skill).Build(this.Source.EntityData, entityData);
    }
}

}