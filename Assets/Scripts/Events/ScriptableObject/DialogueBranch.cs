
using System.Collections.Generic;

[System.Serializable]
public class DialogueBranch {
    // User means the player gets to choose an option
    // GameEvent means that the game automatically proceeds down the tree using some condition
    public enum ChoiceType { User, GameEvent }

    [System.Serializable]
    public class Choice {
        //Display Text isn't used when ChoiceType = GameEvent
        public string DisplayText;
        public DialogueTree NextDialogue;
    }
    public List<Choice> Choices;
    public ChoiceType Type;

    public bool IsBranching => Choices.Count > 0;
}