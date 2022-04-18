using Global;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Battle.Entities;

namespace Battle.UI.Entities {
public class PhysicalEntity : AbstractEntity {

    private readonly Color friendlyColor = new Color32(150, 150, 255, 0);
    private readonly Color hostileColor = new Color32(255, 150, 150, 0);
    private const float onHoverAlpha = 146;
    private const float defaultAlpha = 100;

    public bool IsFriendly;
    [SerializeField]
    private StatsDisplayLite attributesDisplay = default;
    [SerializeField]
    private Image backgroundImage = default;

    public Entity EntityData { get; set; }
    private UnityEvent onClick;

    public static PhysicalEntity Instantiate(GameObject parent, PhysicalEntity prefab,
        bool isFriendly, Entity entityData, Vector3 localPos) {
        PhysicalEntity entity = UnityEngine.Object.Instantiate(prefab, parent.transform, false);
        entity.IsFriendly = isFriendly;
        entity.EntityData = entityData;
        entity.transform.localPosition = localPos;
        return entity;
    }

    // Start is called before the first frame update
    void Awake() {
        onClick = new UnityEvent();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        bool hitCollider = hit.collider?.gameObject == this.gameObject;
        InvokeOnClick(hitCollider);
        ToggleAttributeDisplay(hitCollider);

        attributesDisplay.UpdateStats(EntityData);
    }

    public override void AddOnClickCallback(UnityAction action) {
        onClick.AddListener(action);
    }

    private void InvokeOnClick(bool hitCollider) {
        if(hitCollider && InputHelper.Interact()) {
            onClick.Invoke();
        }
    }

    private void ToggleAttributeDisplay(bool hitCollider) {
        Color32 color = IsFriendly ? friendlyColor : hostileColor;
        color.a = (byte) (hitCollider ? onHoverAlpha : defaultAlpha);
        backgroundImage.color = color;
    }
}
}