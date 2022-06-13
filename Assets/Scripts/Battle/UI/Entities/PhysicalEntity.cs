using Global;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Battle.Entities;

namespace Battle.UI.Entities {

// Represents a space in the battle grid
public class PhysicalEntity : AbstractEntity {

    private readonly Color friendlyColor = new Color32(150, 150, 255, 0);
    private readonly Color hostileColor = new Color32(255, 150, 150, 0);
    private const float onHoverAlpha = 146;
    private const float defaultAlpha = 100;

    public bool IsFriendly;


    public Entity EntityData { get; set; }

    public PhysicalEntity(bool isFriendly, Entity entityData) {
        this.IsFriendly = isFriendly;
        this.EntityData = entityData;
    }

    public override void ToggleAttributeDisplay(Image backgroundImage, bool isHovering) {
        Color32 color = IsFriendly ? friendlyColor : hostileColor;
        color.a = (byte) (isHovering ? onHoverAlpha : defaultAlpha);
        backgroundImage.color = color;
    }

    public override void UpdateFields(StatsDisplayLite statsDisplay) {
        statsDisplay.UpdateStats(EntityData);
    }
}
}