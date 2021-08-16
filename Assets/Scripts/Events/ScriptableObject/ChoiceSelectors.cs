using Global;
using System.Collections.Generic;
using UnityEngine;

namespace Events.ScriptableObject {

using Flag = EventFlags.FlagTypes;

[CreateAssetMenu(fileName = "ChoiceSelectors", menuName = "ScriptableObjects/ChoiceSelectors", order = 5)]
public class ChoiceSelectors: UnityEngine.ScriptableObject {

    public void DummySelector(EventFlagsContainer container, ISet<int> choices) {
        choices.Clear();
        choices.Add(0);
    }

    public void DummyGameEventSelector(EventFlagsContainer flagValues, ISet<int> choices) =>
        EventSelectorHelper(choices, flagValues, Flag.Dummy1, Flag.Dummy2);

    public void DummyUserEventSelctor(EventFlagsContainer flagValues, ISet<int> choices) =>
        EventSelectorHelper(choices, flagValues, Flag.Dummy3, Flag.Dummy3, Flag.Dummy4);

    //returns choices with non-zero eventFlags in the given choices array
    private void EventSelectorHelper(ISet<int> choices, EventFlagsContainer flagValues, params EventFlags.FlagTypes[] flagKeys) {
        choices.Clear();
        for(int i = 0; i < flagKeys.Length; i++) {
            if(flagValues.GetFlag(flagKeys[i]) != 0) {
                choices.Add(i);
            }
        }
    }
}

}