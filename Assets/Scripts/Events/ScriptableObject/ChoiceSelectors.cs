using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChoiceSelectors", menuName = "ScriptableObjects/ChoiceSelectors", order = 5)]
public class ChoiceSelectors: ScriptableObject {


    public void DummySelector(EventFlagsContainer container, ISet<int> choices) {
        choices.Clear();
        choices.Add(0);
    }
}