using System.Collections.Generic;
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

        foreach(AbstractSkillSelectConfig skillConfig in plannedSkills.Values) {
            if(skillConfig.Source.PlayerControlled) {
                Text plannedSkillUIField = Instantiate(plannedSkillUIFieldPrefab, this.transform).GetComponent<Text>();
                plannedSkillUIField.text = skillConfig.DisplayText;
            }
        }
    }
}

}