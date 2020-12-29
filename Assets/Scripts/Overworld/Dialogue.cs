using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    private const float timePerChar = 0.1f;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Image background;

    private bool buttonPressed;

    // Start is called before the first frame update
    void Start() {
        SetActive(false);

        nextButton.onClick.AddListener(() => {
            buttonPressed = true;
        });
    }

    // Update is called once per frame
    void Update() {

    }

    public void Initiate(string[] messages, Action onComplete) {

        //print out a message's characters in sequence
        IEnumerator AutotypeMessage(string message) {
            text.text = "";
            foreach(char letter in message.ToCharArray()) {
                yield return new WaitForSeconds(timePerChar);

                if(buttonPressed) {
                    buttonPressed = false;
                    break;
                }
                text.text += letter;
            }
            text.text = message;
        }

        IEnumerator EngageDialogue() {
            foreach (string message in messages) {
                buttonPressed = false;                
                yield return AutotypeMessage(message);
                yield return new WaitUntil(() => buttonPressed);
            }
            SetActive(false);
            onComplete();
        }

        SetActive(true);
        StartCoroutine(EngageDialogue());
    } 

    private void SetActive(bool active) {
        text.enabled = active;
        nextButton.enabled = active;
        background.enabled = active;
    }
}
