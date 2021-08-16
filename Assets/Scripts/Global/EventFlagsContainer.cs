using Events.ScriptableObject;
using Helpers;
using UnityEngine;

namespace Global {

public class EventFlagsContainer : MonoBehaviour {
    [SerializeField]
    private EventFlags startingFlagValues = default;
    [SerializeField]
    private GenericDictionary<EventFlags.FlagTypes, int> flagValues = default;
    
    void Start() {
        if(startingFlagValues != null) {
            this.flagValues = startingFlagValues.flagValues;
        }
    }

    public int GetFlag(EventFlags.FlagTypes type) {
        if(flagValues.ContainsKey(type)) {
            return flagValues[type];
        } else {
            return 0;
        }
        
    }

    public void SetFlag(EventFlags.FlagTypes type, int val) {
        flagValues[type] = val;
    }

}

}