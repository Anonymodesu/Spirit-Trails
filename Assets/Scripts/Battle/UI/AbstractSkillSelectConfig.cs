using Battle.Entities;

namespace Battle.UI
{
    
abstract class AbstractSkillSelectConfig {
    public Entity Source { get; set; }
    public Skill Skill { get; set; }

    public abstract string DisplayText { get; }
}
}