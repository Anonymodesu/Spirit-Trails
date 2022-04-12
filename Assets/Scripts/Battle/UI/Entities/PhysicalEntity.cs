using Global;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Battle.Entities.Stats;


namespace Battle.UI.Entities {
public class PhysicalEntity : AbstractEntity {

    public bool IsFriendly;
    [SerializeField]
    private GameObject attributesDisplay = default;
    [SerializeField]
    private Text healthDisplay = default;
    [SerializeField]
    private Text manaDisplay = default;
    [SerializeField]
    private Text titleDisplay = default;
    [SerializeField]
    private Image backgroundImage = default;
    public Battle.Entities.Entity EntityData { get; set; }
    private UnityEvent onClick;


    // Start is called before the first frame update
    void Start() {
        
    }

    void Awake() {
        onClick = new UnityEvent();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        bool hitCollider = hit.collider?.gameObject == this.gameObject;
        InvokeOnClick(hitCollider);
        ToggleAttributeDisplay(hitCollider);

        updateEntityStats();
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
        byte alpha = (byte) (hitCollider ? 146 : 100);
        backgroundImage.color = new Color32(255, 255, 255, alpha);
    }

    private void updateEntityStats() {
        EntityStats stats = EntityData.EntityStats;
        titleDisplay.text = EntityData.Name;
        healthDisplay.text = $"{stats.Stamina}/{stats.CurrentHealth}/{stats.MaxHealth}";
        manaDisplay.text = $"{stats.CurrentMana}/{stats.MaxMana}";
    }

}
}