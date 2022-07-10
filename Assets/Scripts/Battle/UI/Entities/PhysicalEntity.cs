using UnityEngine;
using UnityEngine.UI;
using Battle.Entities.Stats;
using Battle.Entities;
using Battle.Effects;

namespace Battle.UI.Entities {

// Represents a space in the battle grid
public class PhysicalEntity : AbstractEntity {

    private readonly Color friendlyColor = new Color32(150, 150, 255, 0);
    private readonly Color hostileColor = new Color32(255, 150, 150, 0);
    private const float onHoverAlpha = 146;
    private const float defaultAlpha = 100;

    public override string Name => EntityData.Name;
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

    public override void Interact(MagicDamage effect) {
        EntityStats currentStats = EntityData.EntityStats;
        int damage = effect.CalculateDamage(currentStats);
        damage = (damage < 0) ? 0 : damage;
        EntityData.EntityStats = currentStats.AdjustHealth(-damage);
    }

    public override void Interact(PhysicalDamage effect) {
        EntityStats currentStats = EntityData.EntityStats;
        int damage = effect.CalculateDamage(currentStats);
        damage = (damage < 0) ? 0 : damage;
        EntityData.EntityStats = currentStats.AdjustHealth(-damage);
    }
}
}