using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// The user selects how to proceed down the tree
[CreateAssetMenu(fileName = "UserDialogueTree", menuName = "ScriptableObjects/UserDialogueTree", order = 3)]
public class UserDialogueTree : DialogueTree {

    [SerializeField]
    private UserDialogueBranch branch;
    public override IDialogueBranch Branch => branch;
}