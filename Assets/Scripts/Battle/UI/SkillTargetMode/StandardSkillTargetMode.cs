using Battle.Controller;
using Battle.Entities;
using Battle.Skills;
using Battle.UI.SkillSelectConfig;
using System.Collections;
using System;
using UnityEngine;

namespace Battle.UI.SkillTargetMode
{
    
class StandardSkillTargetMode : ISkillTargetMode {
    private Entity sourceEntity;
    private Entity targetEntity;


    private Action<AbstractSkillSelectConfig> completeSkillSelection;


    public StandardSkillTargetMode(BattleController controller, Action<AbstractSkillSelectConfig> completeSkillSelection) {
        this.completeSkillSelection = completeSkillSelection;
        controller.EntityGrid.AddEntityOnClick((entity) => {
            switch (controller.BattleState) {
                case BattleState.SelectSkill:
                    sourceEntity = entity;
                    break;
                case BattleState.SelectSkillTarget:
                    targetEntity = entity;
                    break;
            }
        });
    }
    
    public IEnumerator InitiateTargeting(SingleTargetAttackSkill skill) {
        yield return WaitForTargetClick();
        var skillSelectConfig = new SingleTargetSkillSelectConfig {
            Source = sourceEntity,
            Target = targetEntity,
            Skill = skill
        };
        completeSkillSelection(skillSelectConfig);
    }

    public IEnumerator InitiateTargeting(NoTargetSkill skill) {
        yield return null;
        completeSkillSelection(new NoTargetSkillSelectConfig {
            Source = sourceEntity,
            Skill = skill,
        });
    }

    private IEnumerator WaitForTargetClick() {
        targetEntity = null;
        yield return new WaitUntil(() => targetEntity != null);
    }

    
}
}