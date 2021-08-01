using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour {

    private const float timePerChar = 0.1f;
    [SerializeField]
    private Text text = default;
    [SerializeField]
    private Button nextButton = default;
    [SerializeField]
    private Image background = default;
    [SerializeField]
    private GameObject choicesDisplay = default;
    [SerializeField]
    private Button choiceButton = default;
    private EventFlagsContainer eventFlags;

    // Start is called before the first frame update
    void Start() {
        SetActive(false);
        eventFlags = GameObject.FindGameObjectWithTag("MultiSceneData").GetComponent<EventFlagsContainer>();
    }

    // Update is called once per frame
    void Update() {

    }

    //print out a message's characters in sequence
    private IEnumerator AutotypeMessage(string message) {
        bool buttonPressed = false;
        UnityAction action = () => {
            buttonPressed = true;
        };
        nextButton.onClick.AddListener(action); 

        text.text = "";
        foreach (char letter in message.ToCharArray()) {
            yield return new WaitForSeconds(timePerChar);

            if (buttonPressed) {
                buttonPressed = false;
                break;
            }
            text.text += letter;
        }
        text.text = message;
        nextButton.onClick.RemoveListener(action);
    }

    public IEnumerator ActivateChoices(GameEventDialogueBranch gameEventBranch) {
        DialogueTree nextTree = gameEventBranch.GetChoice(eventFlags).NextDialogue;
        yield return EngageDialogue(nextTree);
    }

    public IEnumerator ActivateChoices(UserDialogueBranch userBranch) {
        DialogueTree nextTree = default;
        var choices = userBranch.GetChoices(eventFlags);

        foreach (UserDialogueBranch.Choice choice in choices) {
            Button button = Instantiate(choiceButton, choicesDisplay.transform);
            button.GetComponentInChildren<Text>().text = choice.DisplayText;
            button.onClick.AddListener(() => { nextTree = choice.NextDialogue; });
        }

        choicesDisplay.SetActive(true);
        yield return new WaitUntil(() => nextTree != null);
        choicesDisplay.SetActive(false);

        // Delete previous children from the list of choices i.e. any previous choice buttons
        foreach (Transform choiceButton in choicesDisplay.transform) {
            Destroy(choiceButton.gameObject);
        }
        yield return EngageDialogue(nextTree);
    }
        

    // Step through all the dialogue in the tree
    private IEnumerator EngageDialogue(DialogueTree tree) {
        tree.Event.Invoke();

        foreach (DialogueElement message in tree.DialogItems) {
            yield return AutotypeMessage(message.DialogueText);

            // Wait for the button to be pressed before advancing
            bool buttonPressed = false;
            UnityAction action = () => {
                buttonPressed = true;
            };
            nextButton.onClick.AddListener(action);
            yield return new WaitUntil(() => buttonPressed);
            nextButton.onClick.RemoveListener(action);
        }
        
        if(tree.Branch.IsBranching()) {
            // This will eventually call one of the implementations of ActivateChoices()
            yield return tree.Branch.Accept(this);
        }
    }

    public IEnumerator Initiate(DialogueTree messages, Action onComplete) {
        SetActive(true);
        yield return EngageDialogue(messages);
        SetActive(false);
        onComplete();
    }

    private void SetActive(bool active) {
        this.gameObject.SetActive(active);
    }

}
