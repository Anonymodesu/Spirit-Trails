using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DialogueTree : ScriptableObject {
    public List<DialogueElement> DialogItems;
    //The name of the optional event lambda which is called when this DialogueTree is encountered
    public string EventName;
    public abstract IDialogueBranch Branch { get; }
}