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
    private Events eventsHandler;

    // Start is called before the first frame update
    void Start() {
        SetActive(false);
        eventsHandler = GameObject.FindGameObjectWithTag("MultiSceneData").GetComponent<Events>();
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

    // Displays the choices at the end of the DialogueTree
    private IEnumerator ActivateChoices(DialogueTree messages) {
        DialogueBranch branch = messages.Branch;
        DialogueTree nextTree = default;

        // Instantiate new choice buttons for the player to select
        if (branch.Type == DialogueBranch.ChoiceType.User) {
            var choices = eventsHandler.GetUsersChoices(messages);

            foreach (DialogueBranch.Choice choice in choices) {
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

        // Game automatically selects a path
        } else if(branch.Type == DialogueBranch.ChoiceType.GameEvent) {
            nextTree = eventsHandler.GetGameEventChoice(messages).NextDialogue;
        }

        yield return EngageDialogue(nextTree);
    }

    // Step through all the dialogue in the tree
    private IEnumerator EngageDialogue(DialogueTree messages) {
        foreach (DialogueElement message in messages.DialogItems) {
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
        
        if(messages.Branch.IsBranching) {
            yield return ActivateChoices(messages);
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
