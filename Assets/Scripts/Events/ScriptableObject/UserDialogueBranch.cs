
using Global;
using Overworld;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Events.ScriptableObject {

[System.Serializable]
public class UserDialogueBranch : IDialogueBranch {

    [System.Serializable]
    public class Choice {
        public string DisplayText;
        public DialogueTree NextDialogue;
    }

    public IEnumerator Accept(Dialogue dialogue) =>
        dialogue.ActivateChoices(this);
    
    [SerializeField]
    private UnityFunc choiceSelector = default;
    public List<Choice> GetChoices(EventFlagsContainer eventFlags)
        => choiceSelector.GetChoices(eventFlags, Choices);

    public bool IsBranching() => Choices.Count > 0;

    public List<Choice> Choices;
    public void Reset() => choiceSelector.Reset();

    public UserDialogueBranch() {
        Choices = new List<Choice>();
        choiceSelector = new UnityFunc();
    }
}

}