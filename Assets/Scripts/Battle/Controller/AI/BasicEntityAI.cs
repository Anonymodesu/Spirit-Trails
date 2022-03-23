using Battle.UI.Entities;
using Battle.UI.SkillSelectConfig;
using Battle.UI;
using Battle.Entities;
using Battle.Skills;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Battle.Controller.AI {

class BasicEntityAI: AbstractEntityAI {

    private class SkillTargeting: ISkillTargetMode {
        private PhysicalEntity source;
        private EntityGrid entityGrid;
        private Random random;

        public SkillTargeting(PhysicalEntity entity, EntityGrid entityGrid) {
            this.source = entity;
            this.entityGrid = entityGrid;
            this.random = new Random();
        }

        public ISkillSelectConfig InitiateTargeting(SingleTargetAttackSkill skill) {
            List<PhysicalEntity> entities = entityGrid.ToList<PhysicalEntity>();
            PhysicalEntity randomTarget = entities[random.Next(entities.Count)];
            return new SingleTargetSkillSelectConfig(skill, source, randomTarget);
        }
        public ISkillSelectConfig InitiateTargeting(NoTargetSkill skill) {
            return new NoTargetSkillSelectConfig(skill, source);
        }
    }


    private Random random;
    private ISkillSelectConfig currentConfig;
    private EntityGrid entityGrid;

    public BasicEntityAI() {
        random = new Random();
    }
    public override ISkillSelectConfig SelectSkill(PhysicalEntity entity, EntityGrid entityGrid) {
        List<Skill> skills = entity.EntityData.Skills;
        Skill randomSkill = skills[random.Next(skills.Count)];
        SkillTargeting skillTargeting = new SkillTargeting(entity, entityGrid);
        return randomSkill.InitiateSkillTargeting(skillTargeting);
        
    }

}

}