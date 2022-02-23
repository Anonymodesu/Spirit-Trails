using Battle.Entities;
using Battle.UI.Entities;

namespace Battle.UI
{
    
abstract class AbstractSkillSelectConfig {
    public PhysicalEntity Source { get; protected set; }
    public Skill Skill { get; protected set; }

    public abstract string DisplayText { get; }
    public abstract IEffect Build(); 
}
}