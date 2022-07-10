using UnityEngine.UI;
using Battle.UI.Entities;
using Battle.Effects;

namespace Battle.UI {

public abstract class AbstractEntity {
    public abstract string Name { get; }
    public abstract void ToggleAttributeDisplay(Image backgroundImage, bool isHovering);    
    public abstract void UpdateFields(StatsDisplayLite statsDisplay);

    public abstract void Interact(MagicDamage effect);

    public abstract void Interact(PhysicalDamage effect);

}
}