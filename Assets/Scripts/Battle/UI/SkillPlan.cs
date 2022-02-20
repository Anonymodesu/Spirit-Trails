using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{

class SkillPlan : MonoBehaviour {
    private Dictionary<string, AbstractSkillSelectConfig> plannedSkills;
    [SerializeField]
    private GameObject plannedSkillUIFieldPrefab;

    void Start() {
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
        IEnumerable<AbstractSkillSelectConfig> sortedSkills = plannedSkills.Values.OrderByDescending(skill => skill.Source.EntityData.EntityStats.Speed);
        foreach(AbstractSkillSelectConfig skillConfig in sortedSkills) {
            if(skillConfig.Source.PlayerControlled) {
                Text plannedSkillUIField = Instantiate(plannedSkillUIFieldPrefab, this.transform).GetComponent<Text>();
                plannedSkillUIField.text = skillConfig.DisplayText;
            }
        }
    }
}

}