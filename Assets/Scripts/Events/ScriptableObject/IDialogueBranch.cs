using Overworld;
using System.Collections;

namespace Events.ScriptableObject {

public interface IDialogueBranch {
    IEnumerator Accept(Dialogue dialogue);
    bool IsBranching();
    void Reset();
}

}