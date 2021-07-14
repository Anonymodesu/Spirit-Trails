
using System.Collections.Generic;
using System.Collections;
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
    private string name = default;

    public string Name { get => name; }
    public bool IsBranching() => Choices.Count > 0;

    public List<Choice> Choices;
}