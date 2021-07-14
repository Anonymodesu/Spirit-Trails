using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// The game automatically proceeds down the tree using some condition
[CreateAssetMenu(fileName = "GameEventDialogueTree", menuName = "ScriptableObjects/GameEventDialogueTree", order = 2)]
public class GameEventDialogueTree : DialogueTree {

    [SerializeField]
    private GameEventDialogueBranch branch;
    public override IDialogueBranch Branch => branch;
}