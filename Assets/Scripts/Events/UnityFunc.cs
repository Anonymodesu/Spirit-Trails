using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;

[System.Serializable]
public class UnityFunc {
    [SerializeField]
    private UnityEvent<EventFlagsContainer, ISet<int>> choiceSelectorInvoker;

    public List<T> GetChoices<T>(EventFlagsContainer eventFlags, List<T> allChoices) {
        
        switch(choiceSelectorInvoker.GetPersistentEventCount()) {
            case 0:
                return allChoices;

            case 1:
                ISet<int> choiceIndices = new HashSet<int>();
                choiceSelectorInvoker.Invoke(eventFlags, choiceIndices);

                List<T> availableChoices = new List<T>();
                foreach(int i in choiceIndices) {
                    availableChoices.Add(allChoices[i]);
                }
                return availableChoices;

            default:
                Debug.LogError($"Number of choice selector listeners is greater than 1!");
                return allChoices;
        }
    }

    public UnityFunc(){
        choiceSelectorInvoker = new UnityEvent<EventFlagsContainer, ISet<int>>();
    }

    public void Reset() {
        choiceSelectorInvoker = new UnityEvent<EventFlagsContainer, ISet<int>>();
        string[] assetGUIDs = AssetDatabase.FindAssets("t:ChoiceSelectors");
        string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[0]);
        ChoiceSelectors choiceSelectors =  AssetDatabase.LoadAssetAtPath<ChoiceSelectors>(path);

        UnityEventTools.AddPersistentListener(choiceSelectorInvoker, choiceSelectors.DummySelector);
    }
}