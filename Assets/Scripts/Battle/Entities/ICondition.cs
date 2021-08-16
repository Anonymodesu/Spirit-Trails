
namespace Battle.Entities {
    public interface ICondition {
        bool IsSatisfied(Entity source);
    }
}
