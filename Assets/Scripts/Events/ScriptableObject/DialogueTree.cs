using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueTree", menuName = "ScriptableObjects/DialogueTree", order = 1)]
public class DialogueTree : ScriptableObject {
    public List<DialogueElement> DialogItems;
    //The name of the optional event lambda which is called when this DialogueTree is encountered
    public string EventName;
    public DialogueChoice Branch;
}