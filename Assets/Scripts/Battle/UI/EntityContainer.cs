using Battle.UI.Entities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Global;

namespace Battle.UI
{
    
class EntityContainer : MonoBehaviour {
    public AbstractEntity Entity { get; set; }
    public int Position { get; }

    [SerializeField]
    private StatsDisplayLite attributesDisplay = default;
    [SerializeField]
    private Image backgroundImage = default;
    // Start is called before the first frame update

    private UnityEvent onClick;

    void Awake() {
        onClick = new UnityEvent();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        bool hitCollider = hit.collider?.gameObject == this.gameObject;
        InvokeOnClick(hitCollider);

        Entity.ToggleAttributeDisplay(backgroundImage, hitCollider);
        Entity.UpdateFields(attributesDisplay);
    }

    public static EntityContainer Instantiate(GameObject parent, EntityContainer prefab,
        AbstractEntity entity, Vector3 localPos) {
        EntityContainer container = UnityEngine.Object.Instantiate(prefab, parent.transform, false);
        container.Entity = entity;
        container.transform.localPosition = localPos;
        return container;
    }

    private void InvokeOnClick(bool hitCollider) {
        if(hitCollider && InputHelper.Interact()) {
            onClick.Invoke();
        }
    }

    public void AddOnClickCallback(UnityAction action) {
        onClick.AddListener(action);
    }

}
}