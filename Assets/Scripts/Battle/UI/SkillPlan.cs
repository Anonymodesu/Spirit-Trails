using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{

class SkillPlan : MonoBehaviour, IEnumerable<ISkillSelectConfig> {

    // Use a Dictionary to constrain 1 skill selected per Entity
    private Dictionary<string, ISkillSelectConfig> plannedSkills;
    [SerializeField]
    private GameObject plannedSkillUIFieldPrefab;

    void Awake() {
        plannedSkills = new Dictionary<string, ISkillSelectConfig>();
    }

    public void SetSkill(ISkillSelectConfig skillConfig) {
        plannedSkills[skillConfig.Source.EntityData.Name] = skillConfig;
        ResetPlannedSkills();
    }

    public void ResetPlannedSkills() {
        foreach(Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // Higher speed values go first.
        foreach(ISkillSelectConfig skillConfig in this) {
            // if(skillConfig.Source.IsFriendly) {
                Text plannedSkillUIField = Instantiate(plannedSkillUIFieldPrefab, this.transform).GetComponent<Text>();
                plannedSkillUIField.text = skillConfig.DisplayText;
            // }
        }
    }

    // Return a list of skill configs sorted by the source entity's speed
    public IEnumerator<ISkillSelectConfig> GetEnumerator() 
        => plannedSkills.Values.OrderByDescending(skill => skill.Source.EntityData.EntityStats.Speed).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}

}