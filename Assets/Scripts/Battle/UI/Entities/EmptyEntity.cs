using UnityEngine.UI;
using Battle.Effects;
using UnityEngine;

namespace Battle.UI.Entities {

public class EmptyEntity: AbstractEntity {
    public override string Name => "Empty Entity";
    public override bool IsFriendly { get; }

    public EmptyEntity(bool isFriendly) {
        this.IsFriendly = isFriendly;
    }

    public override void ToggleAttributeDisplay(Image backgroundImage, bool isHovering) {
        
    }
    public override void UpdateFields(StatsDisplayLite statsDisplay) {

    }

    public override void Interact(MagicDamage effect) {
        Debug.Log($"Magic damage on {Name}");
    }

    public override void Interact(PhysicalDamage effect) {
        Debug.Log($"Physical damage on {Name}");
    }

}
}