using Global;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Battle.Entities.Stats;


namespace Battle.UI {
public class Entity : MonoBehaviour {

    public bool PlayerControlled;
    [SerializeField]
    private GameObject attributesDisplay = default;
    [SerializeField]
    private Text healthDisplay = default;
    [SerializeField]
    private Text manaDisplay = default;
    [SerializeField]
    private Text titleDisplay = default;
    public Battle.Entities.Entity EntityData { get; set; }
    public UnityEvent OnClick { get; private set; }


    // Start is called before the first frame update
    void Start() {
        
    }

    void Awake() {
        OnClick = new UnityEvent();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        bool hitCollider = hit.collider != null 
                        && hit.collider.gameObject == this.gameObject;
        InvokeOnClick(hitCollider);
        ToggleAttributeDisplay(hitCollider);
    }

    private void InvokeOnClick(bool hitCollider) {
        if(hitCollider && InputHelper.Interact()) {
            OnClick.Invoke();
        }
    }

    private void ToggleAttributeDisplay(bool hitCollider) {
        attributesDisplay.SetActive(hitCollider);

        if(hitCollider) {
            EntityStats stats = EntityData.EntityStats;
            titleDisplay.text = EntityData.Name;
            healthDisplay.text = $"{stats.Stamina}/{stats.CurrentHealth}/{stats.MaxHealth}";
            manaDisplay.text = $"{stats.CurrentMana}/{stats.MaxMana}";
        }
    }

}
}