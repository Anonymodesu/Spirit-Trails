using Battle.Skills;
using Battle.UI;
namespace Battle.Entities
{
    
public interface ISkillTargetMode {
    ISkillSelectConfig InitiateTargeting(SingleTargetAttackSkill skill);
    ISkillSelectConfig InitiateTargeting(NoTargetSkill skill);
}
}