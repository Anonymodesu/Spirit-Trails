using Battle.Entities;
using Battle.UI.Entities;
using Global;
using System;
using System.Collections;

namespace Battle.UI.SkillSelectConfig
{

// This class is container for another skill select config that hasn't identified its skill targets yet
class DelayedSkillSelectConfig : ISkillSelectConfig {
    public PhysicalEntity Source { get => SkillSelectConfig.Source; }
    public Skill Skill { get => SkillSelectConfig.Skill; }
    public string DisplayText { get => SkillSelectConfig.DisplayText; }

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


    public DelayedSkillSelectConfig() {
    }

    public void SetSkillSelectConfig(ISkillSelectConfig config) {
        this.skillSelectConfig = config;
    }


    public IEffect Build() {
        return SkillSelectConfig.Build();
    }
}
}