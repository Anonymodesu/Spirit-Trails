using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;

public abstract class DialogueTree : ScriptableObject {
    public List<DialogueElement> DialogItems;
    public abstract IDialogueBranch Branch { get; }
    //An optional event to be triggered upon encountering this dialogue tree
    public UnityEvent Event;

    // Set certain ScriptableObjects to be default values for UnityEvent instance variables
    public virtual void Reset() {
        Event = new UnityEvent();
        string[] assetGUIDs = AssetDatabase.FindAssets("t:Events");
        string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[0]);
        Events events =  AssetDatabase.LoadAssetAtPath<Events>(path);
        UnityEventTools.AddPersistentListener(Event, events.Empty);
    }
}