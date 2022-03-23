using Battle.Entities;
using Battle.UI.Entities;

namespace Battle.UI
{
    
public interface ISkillSelectConfig {
    public PhysicalEntity Source { get; }
    public Skill Skill { get; }

    public abstract string DisplayText { get; }
    public abstract IEffect Build(); 
}
}