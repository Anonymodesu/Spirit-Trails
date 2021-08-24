using Global;
using UnityEngine;

namespace Battle.UI {
public class Entity : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(LayerMask.LayerToName(gameObject.layer));
        RaycastHit2D hit = CursorHelper.RaycastCursor(LayerMask.GetMask("Interactables"));
        if(hit.collider != null && InputHelper.Interact()) {
            Debug.Log("hi");
        }
    }
}
}