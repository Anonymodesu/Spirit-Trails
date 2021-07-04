using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public enum InteractionDirection { Up=4, Down=1, Left=3, Right=2, None=0 }

    [SerializeField]
    private Texture2D mouseOverTexture = default;
    [SerializeField]
    private DialogueTree messages = default;
    private Dialogue dialogue;

    public static int LAYER_MASK {
        get { return LayerMask.GetMask("Interactables"); }
    }

    // Start is called before the first frame update
    void Start() {
        if (gameObject.layer != LayerMask.NameToLayer("Interactables")) {
            Debug.Log(gameObject.name + " has interactable script but not interactable layer");
        }
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
    }

    public void MouseEnter() {
        Vector2 offset = new Vector2(mouseOverTexture.width / 3, mouseOverTexture.height);
        CursorHelper.SetCursor(mouseOverTexture, offset, CursorMode.Auto);
    }

    public void Interact(GameObject source, Action onComplete) {
        ConfigInteractionDirection(source);
        onComplete += () =>  {
            source.GetComponent<Animator>().SetInteger("InteractingDirection", 0);
        };
        StartCoroutine(dialogue.Initiate(messages, onComplete));
    }

    // makes the source and dest sprites face eachother
    private void ConfigInteractionDirection(GameObject source) {

        //calculate the angle of the interaction direction relative to the x axis
        Vector3 displacement = this.transform.position - source.transform.position;
        double angle = (180 / Math.PI) * Math.Atan2(displacement.y, displacement.x);

        //angle ranges from -180 to 180
        if(angle > 135) { // Interactable is left of the source
            SetInteractionDirection(InteractionDirection.Left, source);

        } else if(angle > 45) { // Interactable is above the source
            SetInteractionDirection(InteractionDirection.Up, source);

        } else if(angle > -45) { // Interactable is right of the source
            SetInteractionDirection(InteractionDirection.Right, source);

        } else if(angle > -135) { // Interactable is below the source
            SetInteractionDirection(InteractionDirection.Down, source);

        } else { // Interactable is left of the Player
            SetInteractionDirection(InteractionDirection.Left, source);
        }
    }

    //opposing directions e.g. Up/Down are enums that add up to 5
    private InteractionDirection GetOpposingDirection(InteractionDirection direction) {
        if(direction == InteractionDirection.None) {
            return direction;
        } else {
            return (InteractionDirection) (5 - (int)direction);
        }
    }

    private void SetInteractionDirection(InteractionDirection sourceDir, GameObject source) {
        InteractionDirection destDir = GetOpposingDirection(sourceDir);
        Animator sourceAnimator = source.GetComponent<Animator>();
        Animator destAnimator = this.GetComponent<Animator>();
        sourceAnimator?.SetInteger("InteractingDirection", (int) sourceDir);
        destAnimator?.SetInteger("InteractingDirection", (int) destDir);
    }
}
