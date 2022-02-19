using UnityEngine;
using UnityEngine.Events;

namespace Battle.UI {

public abstract class AbstractEntity : MonoBehaviour {

    public abstract void AddOnClickCallback(UnityAction callback);

}
}