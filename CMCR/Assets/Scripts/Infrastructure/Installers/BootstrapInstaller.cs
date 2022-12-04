using Zenject;

namespace CMCR
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUIPanelConfig();
            BindAssets();
            BindGameFactory();
        }

        private void BindUIPanelConfig()
        {
            Container.Bind<UIPanelAppearanceConfig>().AsSingle().NonLazy();
        }

        private void BindAssets()
        {
            Container.Bind<Assets>().AsSingle().NonLazy();
        }

        private void BindGameFactory()
        {
            Container.Bind<GameFactory>().AsSingle().NonLazy();
        }
    }
}