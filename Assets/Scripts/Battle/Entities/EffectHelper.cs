namespace Battle.Entities {
    public static class EffectHelper {
        public static IEffect Next(this IEffect currentEffect, IEffect nextEffect) => 
            new ChainedEffect(currentEffect, nextEffect);

    }
}
