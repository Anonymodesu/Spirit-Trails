using UnityEngine;
using UnityEngine.UI;
using Battle.Skills;
using Battle.Controller.AI;
using Battle.Entities;
using Battle.UI;
using Battle.UI.Entities;
using Battle.UI.SkillTargetMode;
using Battle.UI.SkillSelectConfig;

namespace Battle.Controller {

    public enum BattleState {
        SelectSkill, SelectSkillTarget
    }

    class BattleController : MonoBehaviour {
        
        public EntityGrid EntityGrid { get; private set; }
        public BattleState BattleState { get; private set; }

        private GameObject battleUI;
        private SkillSelect skillSelect;
        private SkillPlan skillPlan;
        private Button battleButton;
        private ISkillTargetMode skillTargeting;
        private AbstractEntityAI entityAI;

        void Start() {
            EntityGrid = GameObject.Find("EntityGrid").GetComponent<EntityGrid>();
            EntityGrid.RefreshEntities();
            battleUI = GameObject.FindWithTag("BattleUI");
            skillSelect = battleUI.transform.Find("SkillSelect").GetComponent<SkillSelect>();
            skillPlan = battleUI.transform.Find("SkillPlan").GetComponent<SkillPlan>();
            battleButton = battleUI.transform.Find("BattleButton").GetComponent<Button>();

            skillTargeting = new StandardSkillTargetMode(this,
                (skillSelectConfig) => {
                    // ResetPlannedSkills is required for configs wrapped with DelayedSkillSelectConfig
                    skillPlan.ResetPlannedSkills();
                    skillSelect.gameObject.SetActive(false);
                    BattleState = BattleState.SelectSkill;
                }); 
            BattleState = BattleState.SelectSkill;

            EntityGrid.AddEntityOnClick((entity) => {
                if(entity.IsFriendly && BattleState == BattleState.SelectSkill) {
                    skillSelect.gameObject.SetActive(true);
                    skillSelect.SetSkills(entity.EntityData.Skills, skill => {
                        BattleState = BattleState.SelectSkillTarget;
                        var skillSelectConfig = skill.InitiateSkillTargeting(skillTargeting);
                        skillPlan.SetSkill(skillSelectConfig);
                    });
                }
            });

            battleButton.onClick.AddListener(() => {
                foreach(ISkillSelectConfig conf in skillPlan) {
                    conf.Build().Activate();
                }

                Debug.Log("Executed skills! Resetting skills.");
                InitialiseEntitySkills();
            });
            
            entityAI = new BasicEntityAI();
            InitialiseEntitySkills();
        }

        private void InitialiseEntitySkills() {
            foreach(PhysicalEntity entity in EntityGrid) {
                if(entity.IsFriendly) {
                    skillPlan.SetSkill(new NoTargetSkillSelectConfig(new NoAction(), entity));
                } else {
                    skillPlan.SetSkill(entityAI.SelectSkill(entity, EntityGrid));
                }
            }
        }

    }
}