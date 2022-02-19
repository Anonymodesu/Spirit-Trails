using Battle.Entities;
using Battle.UI.Entities;

namespace Battle.UI
{
    
abstract class AbstractSkillSelectConfig {
    public PhysicalEntity Source { get; set; }
    public Skill Skill { get; set; }

    public abstract string DisplayText { get; }
}
}