using Battle.Controller;
using Battle.Entities;
using Battle.Skills;
using Battle.UI.SkillSelectConfig;
using Battle.UI.Entities;
using System.Collections;
using System;
using UnityEngine;

namespace Battle.UI.SkillTargetMode
{
    
class StandardSkillTargetMode : ISkillTargetMode {
    private PhysicalEntity sourceEntity;
    private PhysicalEntity targetEntity;

    private BattleController battleController;
    private Action<ISkillSelectConfig> completeSkillSelection;


    public StandardSkillTargetMode(BattleController controller, Action<ISkillSelectConfig> completeSkillSelection) {
        this.completeSkillSelection = completeSkillSelection;
        this.battleController = controller;
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
    
    public ISkillSelectConfig InitiateTargeting(SingleTargetAttackSkill skill) {
        var skillSelectConfig = new DelayedSkillSelectConfig<SingleTargetSkillSelectConfig, PhysicalEntity>(
            sourceEntity,
            skill,
            (source, skill, target) => new SingleTargetSkillSelectConfig((SingleTargetAttackSkill) skill, source, target)
        );

        IEnumerator GetTarget() {
            yield return WaitForTargetClick();
            skillSelectConfig.SetTarget(targetEntity);
            completeSkillSelection(skillSelectConfig);
        }
        
        battleController.StartCoroutine(GetTarget());
        return skillSelectConfig;
    }

    public ISkillSelectConfig InitiateTargeting(NoTargetSkill skill) {
        var skillSelectConfig = new NoTargetSkillSelectConfig(skill, sourceEntity);
        completeSkillSelection(skillSelectConfig);
        return skillSelectConfig;
    }

    private IEnumerator WaitForTargetClick() {
        targetEntity = null;
        yield return new WaitUntil(() => targetEntity != null);
    }

    
}
}