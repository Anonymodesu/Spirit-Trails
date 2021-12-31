using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{

class SkillPlan : MonoBehaviour {
    private Dictionary<string, SkillSelectConfig> plannedSkills;
    [SerializeField]
    private GameObject plannedSkillUIFieldPrefab;

    void Start() {
        plannedSkills = new Dictionary<string, SkillSelectConfig>();
    }

    public void SetSkill(SkillSelectConfig skillConfig) {
        plannedSkills[skillConfig.Source.EntityData.Name] = skillConfig;
        ResetPlannedSkills();
    }

    private void ResetPlannedSkills() {
        foreach(Transform child in this.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(SkillSelectConfig skillConfig in plannedSkills.Values) {
            Text plannedSkillUIField = Instantiate(plannedSkillUIFieldPrefab, this.transform).GetComponent<Text>();
            plannedSkillUIField.text = skillConfig.ToString();
        }
    }
}

}