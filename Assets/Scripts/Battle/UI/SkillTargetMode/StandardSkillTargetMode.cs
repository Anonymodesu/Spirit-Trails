using Battle.Controller;
using Battle.Entities;
using Battle.Skills;
using Battle.UI.SkillSelectConfig;
using Battle.UI.Entities;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle.UI.SkillTargetMode
{
    
class StandardSkillTargetMode : ISkillTargetMode {
    private PhysicalEntity sourceEntity;
    private EntityContainer targetContainer;

    private BattleController battleController;
    private Action completeSkillSelection;


    public StandardSkillTargetMode(BattleController controller, Action completeSkillSelection) {
        this.completeSkillSelection = completeSkillSelection;
        this.battleController = controller;
        controller.EntityGrid.AddPhysicalEntityOnClick(entity => {
            if (controller.BattleState == BattleState.SelectSkill) {
                    sourceEntity = entity;
            }
        });

        controller.EntityGrid.AddContainerOnClick(entity => {
            if (controller.BattleState == BattleState.SelectSkillTarget) {
                targetContainer = entity;
            }
        });
    }
    
    public ISkillSelectConfig InitiateTargeting(SingleTargetSkill skill) {
        var skillSelectConfig = new DelayedSkillSelectConfig<EntityContainer>(
            sourceEntity,
            skill,
            (source, skill, target) => new SingleTargetSkillSelectConfig((SingleTargetSkill) skill, source, target)
        );

        IEnumerator GetTarget() {
            yield return WaitForTargetClick();
            skillSelectConfig.SetTarget(targetContainer);
            completeSkillSelection();
        }
        
        battleController.StartCoroutine(GetTarget());
        return skillSelectConfig;
    }

    public ISkillSelectConfig InitiateTargeting(AoESkill skill) {
        var skillSelectConfig = new DelayedSkillSelectConfig<IEnumerable<EntityContainer>>(
            sourceEntity,
            skill,
            (source, skill, target) => new AoESkillSelectConfig((AoESkill) skill, source, target)
        );

        IEnumerator GetTarget() {
            yield return WaitForTargetClick();
            var surroundingEntities = battleController.EntityGrid.GetEntities(
                targetContainer.Entity.IsFriendly, 
                targetContainer.Position - skill.Radius, 
                targetContainer.Position + skill.Radius
            );
            skillSelectConfig.SetTarget(surroundingEntities);
            completeSkillSelection();
        }

        battleController.StartCoroutine(GetTarget());
        return skillSelectConfig;
    }

    public ISkillSelectConfig InitiateTargeting(NoTargetSkill skill) {
        var skillSelectConfig = new NoTargetSkillSelectConfig(skill, sourceEntity);
        completeSkillSelection();
        return skillSelectConfig;
    }

    private IEnumerator WaitForTargetClick() {
        targetContainer = null;
        yield return new WaitUntil(() => targetContainer != null);
    }

    
}
}