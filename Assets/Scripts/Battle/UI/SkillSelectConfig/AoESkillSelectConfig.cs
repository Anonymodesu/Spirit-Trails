using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Battle.UI.SkillSelectConfig
{
    
public class AoESkillSelectConfig : TargettedSkillSelectConfig<IEnumerable<EntityContainer>> {

    public AoESkillSelectConfig(AoESkill skill, PhysicalEntity source, IEnumerable<EntityContainer> targets) {
        this.Source = source;
        this.Skill = skill;
        this.Target = targets;
    }

    // Extracts a property from the list of EntityContainers
    private List<Prop> GetTargetEntityProperty<Prop>(Func<EntityContainer, Prop> propGetter) =>
        Target.Select(propGetter).ToList();

    public override string DisplayText {
        get {
            String targetNames = String.Join(",", GetTargetEntityProperty(e => e.Entity.Name));
            return $"{Source.EntityData.Name} - {Skill.Name} -> {targetNames}";
        }
    }

    public override IEffect Build() {
        var entities = GetTargetEntityProperty(e => e.Entity);
        return ((AoESkill) Skill).Build(this.Source.EntityData, entities);
    }
}

}