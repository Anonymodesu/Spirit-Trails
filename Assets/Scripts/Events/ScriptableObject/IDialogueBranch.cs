

using System.Collections;

public interface IDialogueBranch {
    IEnumerator Accept(Dialogue dialogue);
    string Name { get; }
    bool IsBranching();
}