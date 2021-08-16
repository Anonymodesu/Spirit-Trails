using Battle.Entities.Stats;
namespace Battle.Entities {
    class ChainedEffect: IEffect {

        private IEffect currentEffect;
        private IEffect nextEffect;
        public ChainedEffect(IEffect currentEffect, IEffect nextEffect) {
            this.currentEffect = currentEffect;
            this.nextEffect = nextEffect;
        }

        public void Activate() {
            currentEffect.Activate();
            nextEffect.Activate();
        }

    }
}
