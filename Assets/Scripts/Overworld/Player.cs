using Global;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld {

public class Player : MonoBehaviour
{
    private static readonly float speed = 0.1f;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator animator;
    private MovementInput movement;
    private HashSet<Interactable> nearbyInteractables;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementInput>();
        nearbyInteractables = new HashSet<Interactable>(); 
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
        CheckInteractables();
    }

    private void CheckInteractables() {
        void resetCursor() {
            CursorHelper.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        RaycastHit2D hit = CursorHelper.RaycastCursor(Interactable.LAYER_MASK);
        if(hit.collider != null && nearbyInteractables.Contains(hit.collider.GetComponent<Interactable>())) { 
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if(nearbyInteractables.Contains(interactable)) {

                if(InputHelper.Interact()) {
                    enabled = false;
                    resetCursor();
                    animator.SetInteger("WalkingDirection", (int) MovementInput.WalkingDirection.None);
                    interactable.Interact(this.gameObject, () => {
                        enabled = true;
                    });

                } else {
                    interactable.MouseEnter();
                }

            } else {
                resetCursor();
            }
        } else {
            resetCursor();
        }
    }

    private void UpdateMovement() {
        if(enabled) {
            animator.SetInteger("WalkingDirection",(int) movement.GetAnimationDirection());
            mainCamera.transform.position = this.transform.position - Vector3.forward;
        }
    }

    void FixedUpdate() {
        // update sprite position
        Vector3 movementDelta = speed * (movement.GetMovementDirection()).normalized;
        rb.MovePosition(this.transform.position + movementDelta);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null) {
            nearbyInteractables.Add(interactable);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null) {
            nearbyInteractables.Remove(interactable);
        }
    }
    
}

}