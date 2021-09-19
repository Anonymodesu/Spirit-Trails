using Global;
using UnityEngine;
using UnityEngine.UI;
using Battle.Entities.Stats;
using Battle.Entities;


namespace Battle.UI {
public class Entity : MonoBehaviour {

    [SerializeField]
    private GameObject attributesDisplay = default;
    [SerializeField]
    private Text healthDisplay = default;
    [SerializeField]
    private Text manaDisplay = default;
    private GameObject battleUI;
    private Battle.Entities.Entity entity;


    // Start is called before the first frame update
    void Start() {
        battleUI = GameObject.FindGameObjectWithTag("BattleUI");
        entity = GameObject.FindGameObjectWithTag("MultiSceneData").GetComponent<PlayerStats>().entity;
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(LayerMask.LayerToName(gameObject.layer));
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        bool hitCollider = hit.collider != null;

        if(hitCollider && InputHelper.Interact()) {
            battleUI.SetActive(!battleUI.activeSelf);
        }
        attributesDisplay.SetActive(hitCollider);
        
        SetAttributes();
    }

    private void SetAttributes() {
        EntityStats stats= entity.EntityStats;
        healthDisplay.text = $"{stats.Stamina}/{stats.CurrentHealth}/{stats.MaxHealth}";
        manaDisplay.text = $"{stats.CurrentMana}/{stats.MaxMana}";
    }
}
}