using UnityEngine;

namespace Events.ScriptableObject {

// The user selects how to proceed down the tree
[CreateAssetMenu(fileName = "UserDialogueTree", menuName = "ScriptableObjects/UserDialogueTree", order = 3)]
public class UserDialogueTree : DialogueTree {

    [SerializeField]
    private UserDialogueBranch branch;
    public override IDialogueBranch Branch => branch;

    public override void Reset() {
        base.Reset();

        branch = new UserDialogueBranch();
        branch.Reset();
    }
}

}