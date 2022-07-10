using Battle.Entities;
using Battle.UI.Entities;
using System;

namespace Battle.UI.SkillSelectConfig
{

// This class is container for another skill select config that hasn't identified its skill targets yet
class DelayedSkillSelectConfig<TargetType>: TargettedSkillSelectConfig<TargetType> {

    public override string DisplayText { 
        get => IsReady ? SkillSelectConfig.DisplayText : $"{Source.EntityData.Name} - {Skill.Name} -> ?"; 
    }

    public TargettedSkillSelectConfig<TargetType> SkillSelectConfig {
        get {
            if(IsReady) {
                return skillSelectConfig;
            } else {
                throw new InvalidOperationException("SkillSelectConfig hasn't finished obtaining its targets.");
            }
        }
    }
    public bool IsReady { get => skillSelectConfig != null; }

    private TargettedSkillSelectConfig<TargetType> skillSelectConfig;
    private Func<PhysicalEntity, Skill, TargetType, TargettedSkillSelectConfig<TargetType>> configConstructor;

    public DelayedSkillSelectConfig(
        PhysicalEntity source,
        Skill skill,
        Func<PhysicalEntity, Skill, TargetType, TargettedSkillSelectConfig<TargetType>> configConstructor) {

        this.Source = source;
        this.Skill = skill;
        this.configConstructor = configConstructor;
    }

    public void SetTarget(TargetType target) {
        this.Target = target;
        this.skillSelectConfig = configConstructor(Source, Skill, Target);
    }


    public override IEffect Build() {
        return SkillSelectConfig.Build();
    }
}
}