using Common.Implementations.Systems.Pools;
using Common.Interfaces;
using GameField.Implementations.Systems;
using GameField.Implementations.Views;
using GameField.Interfaces;
using Zenject;

namespace GameField.DI
{
    /// <summary>
    /// DI инстоллер для игрового поля
    /// </summary>
    public class GameFieldInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IMonoBehaviourPool<IGameFieldCellDestroyParticlesView>>().To<MonoBehaviourPool<IGameFieldCellDestroyParticlesView, GameFieldCellDestroyParticlesView>>().AsCached();
            Container.Bind<IMonoBehaviourPool<IGameFieldCellExplosionParticlesView>>().To<MonoBehaviourPool<IGameFieldCellExplosionParticlesView, GameFieldCellExplosionParticlesView>>().AsCached();
            Container.Bind<IGameFieldInteractor>().To<GameFieldInteractor>().AsCached();
            Container.Bind<IGameFieldCreator>().To<GameFieldCreator>().AsCached();
        }
    }
}