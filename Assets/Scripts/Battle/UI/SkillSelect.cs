using Battle.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{

public enum BattleState {
    SelectSkill, SelectSkillTarget
}

public class SkillSelect : MonoBehaviour
{

    [SerializeField]
    private GameObject skillButtonPrefab;
    [SerializeField]
    private SkillPlan skillPlan;
    private GameObject skillButtons;
    private GameObject entityGrid;
    private SkillSelectConfig skillSelectState;
    private BattleState battleState;

    void Start()
    {
        skillButtons = this.transform.Find("SkillButtons").gameObject;
        battleState = BattleState.SelectSkill;
        entityGrid = GameObject.Find("EntityGrid");
        GameObject battleUI = this.transform.parent.gameObject;

        foreach(Transform child in entityGrid.transform) {
            Entity entity = child.GetComponent<Entity>();
            entity.OnClick.AddListener(() => {

                if(entity.PlayerControlled && 
                (this.battleState == BattleState.SelectSkill || this.battleState == BattleState.SelectSkill)) {
                    this.gameObject.SetActive(true);
                    this.SetSkills(entity.EntityData.Skills);
                    this.battleState = BattleState.SelectSkill;
                    skillSelectState = new SkillSelectConfig { Source = entity };

                } else if (this.battleState == BattleState.SelectSkillTarget) {
                    this.gameObject.SetActive(false);
                    this.battleState = BattleState.SelectSkill;
                    skillSelectState.Target = entity;
                    skillPlan.SetSkill(skillSelectState);
                }
            });
        }
    }

    public void SetSkills(IEnumerable<Skill> skills) {
        foreach(Transform child in skillButtons.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Skill skill in skills) {
            Button button = Instantiate(skillButtonPrefab, skillButtons.transform).GetComponent<Button>();
            button.transform.Find("Text").GetComponent<Text>().text = skill.Name;
            button.onClick.AddListener(() => {
                this.battleState = BattleState.SelectSkillTarget;
                skillSelectState.Skill = skill;
            });
        }
    }


}

}