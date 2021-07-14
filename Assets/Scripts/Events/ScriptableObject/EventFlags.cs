using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventFlags", menuName = "ScriptableObjects/EventFlags", order = 1)]
public class EventFlags : ScriptableObject {
    public enum FlagTypes { Dummy1, Dummy2, Dummy3, Dummy4 };
    [SerializeField]
    public GenericDictionary<FlagTypes, int> flagValues = default;
}
