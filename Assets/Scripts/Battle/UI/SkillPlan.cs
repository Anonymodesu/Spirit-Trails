using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{

class SkillPlan : MonoBehaviour, IEnumerable<AbstractSkillSelectConfig> {

    // Use a Dictionary to constrain 1 skill selected per Entity
    private Dictionary<string, AbstractSkillSelectConfig> plannedSkills;
    [SerializeField]
    private GameObject plannedSkillUIFieldPrefab;

    void Awake() {
        plannedSkills = new Dictionary<string, AbstractSkillSelectConfig>();
    }

    public void SetSkill(AbstractSkillSelectConfig skillConfig) {
        plannedSkills[skillConfig.Source.EntityData.Name] = skillConfig;
        ResetPlannedSkills();
    }

    private void ResetPlannedSkills() {
        foreach(Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // Higher speed values go first.
        foreach(AbstractSkillSelectConfig skillConfig in this) {
            if(skillConfig.Source.PlayerControlled) {
                Text plannedSkillUIField = Instantiate(plannedSkillUIFieldPrefab, this.transform).GetComponent<Text>();
                plannedSkillUIField.text = skillConfig.DisplayText;
            }
        }
    }

    // Return a list of skill configs sorted by the source entity's speed
    public IEnumerator<AbstractSkillSelectConfig> GetEnumerator() 
        => plannedSkills.Values.OrderByDescending(skill => skill.Source.EntityData.EntityStats.Speed).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}

}