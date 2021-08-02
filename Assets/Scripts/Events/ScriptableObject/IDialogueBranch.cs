

using System.Collections;

public interface IDialogueBranch {
    IEnumerator Accept(Dialogue dialogue);
    bool IsBranching();
    void Reset();
}