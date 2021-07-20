using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/Events", order = 4)]
public class Events: ScriptableObject {

    public void Empty() {}

    public void Dummy() {
        Debug.Log("Dummy Event has been triggered");
    }
}