using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {
    [SerializeField]
    private EventFlags startingFlagValues = default;
    [SerializeField]
    private GenericDictionary<EventFlags.FlagTypes, int> flagValues = default;

    // Below are mappings of dialogueTree names to functions
    // userChoiceSelector takes a DialogueTree as input and returns potential choices for the user
    // gameChoiceSelector takes a DialogueTree as input and returns a single path for the game to follow
    private Dictionary<string, Func<DialogueTree, List<DialogueBranch.Choice>>> userChoiceSelector = default;
    private Dictionary<string, Func<DialogueTree, DialogueBranch.Choice>> gameChoiceSelector = default;

    void Start() {
        if(startingFlagValues != null) {
            this.flagValues = startingFlagValues.flagValues;
        }
        this.userChoiceSelector = new Dictionary<string, Func<DialogueTree, List<DialogueBranch.Choice>>> {
            {"DummyUserBranch", (tree) => UserEventSelectorHelper(tree.Branch.Choices, EventFlags.FlagTypes.Dummy2, EventFlags.FlagTypes.Dummy1, EventFlags.FlagTypes.Dummy2) }
        };

        this.gameChoiceSelector = new Dictionary<string, Func<DialogueTree, DialogueBranch.Choice>> {
            {"DummyGameBranch", (tree) => GameEventSelectorHelper(tree.Branch.Choices, EventFlags.FlagTypes.Dummy1, EventFlags.FlagTypes.Dummy2, EventFlags.FlagTypes.Dummy3) }
        };
    }

    public int GetFlag(EventFlags.FlagTypes type) {
        if(flagValues.ContainsKey(type)) {
            return flagValues[type];
        } else {
            return 0;
        }
        
    }

    //returns a single choice with a non-zero eventFlag in the given choices array
    private DialogueBranch.Choice GameEventSelectorHelper(List<DialogueBranch.Choice> choices, params EventFlags.FlagTypes[] eventFlags) {
        var availableChoices = UserEventSelectorHelper(choices, eventFlags);

        if(availableChoices.Count != 1) {
            Debug.LogError($"Game event produced more than ${availableChoices.Count} choices");
        }

        return availableChoices.First();
    }
    
    //returns choices with non-zero eventFlags in the given choices array
    private List<DialogueBranch.Choice> UserEventSelectorHelper(List<DialogueBranch.Choice> choices, params EventFlags.FlagTypes[] eventFlags) {
        if(choices.Count != eventFlags.Length) {
            Debug.LogError($"Non-matching flag and choice sizes: {eventFlags.Length} vs {choices.Count}");
        }

        var availableChoices = new List<DialogueBranch.Choice>();
        for(int i = 0; i < choices.Count; i++) {
            if(GetFlag(eventFlags[i]) != 0) {
                availableChoices.Add(choices[i]);
            }
        }

        return availableChoices;
    }

    public void SetFlag(EventFlags.FlagTypes type, int val) {
        flagValues[type] = val;
    }

    public List<DialogueBranch.Choice> GetUsersChoices(DialogueTree dialogue) {
        // return every choice by default
        var availableChoices = dialogue.Branch.Choices;

        if(userChoiceSelector.ContainsKey(dialogue.name)) {
            availableChoices = userChoiceSelector[dialogue.name].Invoke(dialogue);

            if(availableChoices.Count == 0) {
                Debug.LogError($"{dialogue.name} is returning 0 options");
            }

        }

        return availableChoices;
    }

    public DialogueBranch.Choice GetGameEventChoice(DialogueTree dialogue) {
        
        if(gameChoiceSelector.ContainsKey(dialogue.name)) {
            return gameChoiceSelector[dialogue.name].Invoke(dialogue);
        
        // return first choice by default
        } else {
            return dialogue.Branch.Choices.First();
        }
    }
}
