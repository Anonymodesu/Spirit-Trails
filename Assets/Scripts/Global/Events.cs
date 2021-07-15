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
    private Dictionary<string, Func<UserDialogueBranch, List<UserDialogueBranch.Choice>>> userChoiceSelector;
    private Dictionary<string, Func<GameEventDialogueBranch, GameEventDialogueBranch.Choice>> gameChoiceSelector;

    // This maps the EventName in the DialogueTree to an event
    private Dictionary<string, Action> eventSelector;

    void Start() {
        if(startingFlagValues != null) {
            this.flagValues = startingFlagValues.flagValues;
        }
        this.userChoiceSelector = new Dictionary<string, Func<UserDialogueBranch, List<UserDialogueBranch.Choice>>> {
            {"DummyUserBranch", (branch) => EventSelectorHelper(branch.Choices, EventFlags.FlagTypes.Dummy2, EventFlags.FlagTypes.Dummy1, EventFlags.FlagTypes.Dummy2) }
        };

        this.gameChoiceSelector = new Dictionary<string, Func<GameEventDialogueBranch, GameEventDialogueBranch.Choice>> {
            {"DummyGameBranch", (branch) => GameEventSelectorHelper(branch.Choices, EventFlags.FlagTypes.Dummy3, EventFlags.FlagTypes.Dummy4) }
        };
        this.eventSelector = new Dictionary<string, Action> {
            {"", () => {} },
            {"DummyEvent", () => Debug.Log("Dummy event has happened") }
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
    private GameEventDialogueBranch.Choice GameEventSelectorHelper(List<GameEventDialogueBranch.Choice> choices, params EventFlags.FlagTypes[] eventFlags) {
        var availableChoices = EventSelectorHelper(choices, eventFlags);

        if(availableChoices.Count != 1) {
            Debug.LogError($"Game event produced more than ${availableChoices.Count} choices");
        }

        return availableChoices.First();
    }
    
    //returns choices with non-zero eventFlags in the given choices array
    private List<T> EventSelectorHelper<T>(List<T> choices, params EventFlags.FlagTypes[] eventFlags) {
        if(choices.Count != eventFlags.Length) {
            Debug.LogError($"Non-matching flag and choice sizes: {eventFlags.Length} vs {choices.Count}");
        }

        var availableChoices = new List<T>();
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

    public List<UserDialogueBranch.Choice> GetUsersChoices(UserDialogueBranch branch) {
        // return every choice by default
        var availableChoices = branch.Choices;

        if(userChoiceSelector.ContainsKey(branch.Name)) {
            availableChoices = userChoiceSelector[branch.Name].Invoke(branch);

            if(availableChoices.Count == 0) {
                Debug.LogError($"{branch.Name} is returning 0 options");
            }

        }

        return availableChoices;
    }

    public GameEventDialogueBranch.Choice GetGameEventChoice(GameEventDialogueBranch branch) {
        
        if(gameChoiceSelector.ContainsKey(branch.Name)) {
            return gameChoiceSelector[branch.Name].Invoke(branch);
        
        // return first choice by default
        } else {
            return branch.Choices.First();
        }
    }

    public Action GetEvent(DialogueTree tree) =>
        eventSelector[tree.EventName];
}
