using Battle.Entities;
using Battle.UI.Entities;
using System;

namespace Battle.UI.SkillSelectConfig
{

// This class is container for another skill select config that hasn't identified its skill targets yet
class DelayedSkillSelectConfig<ConfigType, TargetType>: TargettedSkillSelectConfig<TargetType> 
                                                        where ConfigType: TargettedSkillSelectConfig<TargetType> {

    public override string DisplayText { 
        get => IsReady ? SkillSelectConfig.DisplayText : $"{Source.EntityData.Name} - {Skill.Name} -> ?"; 
    }

    public ISkillSelectConfig SkillSelectConfig {
        get {
            if(IsReady) {
                return skillSelectConfig;
            } else {
                throw new InvalidOperationException("SkillSelectConfig hasn't finished obtaining its targets.");
            }
        }
    }
    public bool IsReady { get => skillSelectConfig != null; }

    private ISkillSelectConfig skillSelectConfig;
    private Func<PhysicalEntity, Skill, TargetType, ConfigType> configConstructor;

    public DelayedSkillSelectConfig(
        PhysicalEntity source,
        Skill skill,
        Func<PhysicalEntity, Skill, TargetType, ConfigType> configConstructor) {

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