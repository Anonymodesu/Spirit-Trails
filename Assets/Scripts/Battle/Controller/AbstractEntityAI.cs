using Battle.UI.Entities;
using Battle.UI;
using Battle.Skills;
using Battle.Entities;

namespace Battle.Controller {

abstract class AbstractEntityAI  {
    public abstract ISkillSelectConfig SelectSkill(PhysicalEntity entity, EntityGrid entityGrid);
}


}