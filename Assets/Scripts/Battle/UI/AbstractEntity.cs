using UnityEngine.UI;
using Battle.UI.Entities;
using System;

namespace Battle.UI {

public abstract class AbstractEntity {
    public abstract void ToggleAttributeDisplay(Image backgroundImage, bool isHovering);    
    public abstract void UpdateFields(StatsDisplayLite statsDisplay);
}
}