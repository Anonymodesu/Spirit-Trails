using Battle.Skills;
using System.Collections;
using System;

namespace Battle.Entities
{
    
public interface ISkillTargetMode {
    IEnumerator InitiateTargeting(SingleTargetAttackSkill skill);
    IEnumerator InitiateTargeting(NoTargetSkill skill);
}
}