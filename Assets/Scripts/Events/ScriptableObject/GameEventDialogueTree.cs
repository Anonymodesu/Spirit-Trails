using UnityEngine;

namespace Events.ScriptableObject {

// The game automatically proceeds down the tree using some condition
[CreateAssetMenu(fileName = "GameEventDialogueTree", menuName = "ScriptableObjects/GameEventDialogueTree", order = 2)]
public class GameEventDialogueTree : DialogueTree {

    [SerializeField]
    private GameEventDialogueBranch branch;
    public override IDialogueBranch Branch => branch;

    public override void Reset() {
        base.Reset();

        branch = new GameEventDialogueBranch();
        branch.Reset();
    }
}

}