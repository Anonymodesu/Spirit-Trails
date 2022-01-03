using UnityEngine;
using Battle.UI;

namespace Battle.Controller {

    public enum BattleState {
        SelectSkill, SelectSkillTarget
    }

    class BattleController : MonoBehaviour {
        private BattleState battleState;
        public SkillSelectConfig SkillSelectState { get; private set; }


        void Start() {
            EntityGrid entityGrid = GameObject.Find("EntityGrid").GetComponent<EntityGrid>();
            GameObject battleUI = GameObject.FindWithTag("BattleUI");
            SkillSelect skillSelect = battleUI.transform.Find("SkillSelect").GetComponent<SkillSelect>();
            SkillPlan skillPlan = battleUI.transform.Find("SkillPlan").GetComponent<SkillPlan>();

            entityGrid.SetEntities((entity) => {

                if(entity.PlayerControlled && this.battleState == BattleState.SelectSkill) {
                    skillSelect.gameObject.SetActive(true);
                    skillSelect.SetSkills(entity.EntityData.Skills, skill => {
                        this.SkillSelectState.Skill = skill;
                        this.battleState = BattleState.SelectSkillTarget;
                    });
                    this.battleState = BattleState.SelectSkill;
                    this.SkillSelectState = new SkillSelectConfig { Source = entity };

                } else if (this.battleState == BattleState.SelectSkillTarget) {
                    skillSelect.gameObject.SetActive(false);
                    this.battleState = BattleState.SelectSkill;
                    this.SkillSelectState.Target = entity;
                    skillPlan.SetSkill(this.SkillSelectState);
                }
            });
            
        }
    }
}