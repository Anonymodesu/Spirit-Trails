using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour {

    private const float timePerChar = 0.1f;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Image background;
    [SerializeField]
    private GameObject choicesList;
    [SerializeField]
    private Button choiceButton;

    // Start is called before the first frame update
    void Start() {
        SetActive(false);
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

        //Delete previous children from the list of choices i.e. any previous choice buttons
        foreach (Transform choiceButton in choicesList.transform) {
            Destroy(choiceButton.gameObject);
        }

        DialogueChoice choices = messages.Choices;
        if (choices.Choices.Count > 0) {
            DialogueTree nextTree = null;

            //Instantiate new choice buttons
            if (choices.Type == DialogueChoice.ChoiceType.User) {

                foreach (DialogueChoice.Choice choice in choices.Choices) {
                    Button button = Instantiate(choiceButton, choicesList.transform);
                    button.GetComponentInChildren<Text>().text = choice.DisplayText;
                    button.onClick.AddListener(() => { nextTree = choice.NextDialogue; });
                }

                choicesList.SetActive(true);
                yield return new WaitUntil(() => nextTree != null);
                choicesList.SetActive(false);
            }

            yield return EngageDialogue(nextTree);
        }
    }


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
        
        yield return ActivateChoices(messages);
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
