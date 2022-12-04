using UnityEngine;
using Zenject;

namespace CMCR
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private Level _level;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private AlliesPool _alliesPool;
        [SerializeField] private UnitsFactory _unitsFactory;
        [SerializeField] private AlliesGroup _alliesGroup;
        [SerializeField] private CameraFollower _camera;
        [SerializeField] private ParticlePools _particlePools;
        [SerializeField] private StainsSpawner stainsSpawner;
        [SerializeField] private Boss _boss;

        public override void InstallBindings()
        {
            BindGameBootstrapper();
            BindUI();
            BindLevel();
            BindObjectsPool();
            BindUnitsFactory();
            BindStainsSpawner();
            BindDeathParticlesPool();
            BindAlliesGroup();
            BindCamera();
            BindBoss();
        }

        private void BindGameBootstrapper()
        {
            Container.Bind<GameBootstrapper>().FromInstance(_gameBootstrapper).AsSingle().NonLazy();
        }

        private void BindLevel()
        {
            Container.Bind<Level>().FromInstance(_level).AsSingle().NonLazy();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromInstance(_uiRoot).AsSingle();
        }

        private void BindObjectsPool()
        {
            Container.Bind<AlliesPool>().FromInstance(_alliesPool).AsSingle().NonLazy();
        }

        private void BindUnitsFactory()
        {
            Container.Bind<UnitsFactory>().FromInstance(_unitsFactory).AsSingle();
        }

        private void BindStainsSpawner()
        {
            Container.Bind<StainsSpawner>().FromInstance(stainsSpawner).AsSingle().NonLazy();
        }

        private void BindAlliesGroup()
        {
            Container.Bind<AlliesGroup>().FromInstance(_alliesGroup).AsSingle();
        }

        private void BindCamera()
        {
            Container.Bind<CameraFollower>().FromInstance(_camera).AsSingle().NonLazy();
        }

        private void BindDeathParticlesPool()
        {
            Container.Bind<ParticlePools>().FromInstance(_particlePools).AsSingle().NonLazy();
        }

        private void BindBoss()
        {
            Container.Bind<Boss>().FromInstance(_boss).AsSingle().NonLazy();
        }
    }
}