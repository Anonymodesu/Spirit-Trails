

using System.Collections;

public interface IDialogueBranch {
    // User means the player gets to choose an option
    // GameEvent means that the game automatically proceeds down the tree using some condition
    IEnumerator Accept(Dialogue dialogue);
    string Name { get; }
    bool IsBranching();
}