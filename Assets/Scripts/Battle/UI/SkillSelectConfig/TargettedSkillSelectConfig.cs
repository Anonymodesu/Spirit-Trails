using Battle.Entities;
using Battle.Skills;
using Battle.UI.Entities;

namespace Battle.UI.SkillSelectConfig
{
    
public abstract class TargettedSkillSelectConfig<TargetType> : ISkillSelectConfig {

    public PhysicalEntity Source { get; protected set; }
    public Skill Skill { get; protected set; }
    public TargetType Target { get; protected set; }

    public abstract string DisplayText { get; }

    public abstract IEffect Build();
}
}