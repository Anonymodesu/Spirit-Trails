
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameEventDialogueBranch : IDialogueBranch {

    [System.Serializable]
    public class Choice {
        public DialogueTree NextDialogue;
    }

    public IEnumerator Accept(Dialogue dialogue) =>
        dialogue.ActivateChoices(this);

    [SerializeField]
    private UnityFunc choiceSelector;
    public Choice GetChoice(EventFlagsContainer eventFlags) {
        List<Choice> choices = choiceSelector.GetChoices(eventFlags, Choices);
        if(choices.Count > 1) {
            Debug.LogError("Got more than 1 GameEventChoice!");
        }
        return choices.First();
    }

    public bool IsBranching() => Choices.Count > 0;

    public List<Choice> Choices;

    public void Reset() => choiceSelector.Reset();

    public GameEventDialogueBranch() {
        Choices = new List<Choice>();
        choiceSelector = new UnityFunc();
    }
}