using UnityEngine;
using UnityEngine.UI;
using Battle.Skills;
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

        void Start() {
            EntityGrid = GameObject.Find("EntityGrid").GetComponent<EntityGrid>();
            EntityGrid.RefreshEntities();
            battleUI = GameObject.FindWithTag("BattleUI");
            skillSelect = battleUI.transform.Find("SkillSelect").GetComponent<SkillSelect>();
            skillPlan = battleUI.transform.Find("SkillPlan").GetComponent<SkillPlan>();
            battleButton = battleUI.transform.Find("BattleButton").GetComponent<Button>();

            skillTargeting = new StandardSkillTargetMode(this,
                (skillSelectConfig) => {
                    skillPlan.SetSkill(skillSelectConfig);
                    skillSelect.gameObject.SetActive(false);
                    BattleState = BattleState.SelectSkill;
                }); 
            BattleState = BattleState.SelectSkill;

            EntityGrid.AddEntityOnClick((entity) => {
                if(entity.PlayerControlled && BattleState == BattleState.SelectSkill) {
                    skillSelect.gameObject.SetActive(true);
                    skillSelect.SetSkills(entity.EntityData.Skills, skill => {
                        BattleState = BattleState.SelectSkillTarget;
                        StartCoroutine(skill.InitiateSkillTargeting(skillTargeting));
                    });
                }
            });

            battleButton.onClick.AddListener(() => {
                foreach(AbstractSkillSelectConfig conf in skillPlan) {
                    conf.Build().Activate();
                }

                Debug.Log("Executed skills! Resetting skills.");
                InitialiseEntitySkills();
            });
            
            InitialiseEntitySkills();
        }

        private void InitialiseEntitySkills() {
            foreach(PhysicalEntity entity in EntityGrid) {
                skillPlan.SetSkill(new NoTargetSkillSelectConfig(new NoAction(), entity));
            }
        }

    }
}