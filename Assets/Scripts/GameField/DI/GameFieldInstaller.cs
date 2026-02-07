using GameField.Implementations.Systems;
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
            Container.Bind<IGameFieldInteractor>().To<GameFieldInteractor>().AsCached();
            Container.Bind<IGameFieldCreator>().To<GameFieldCreator>().AsCached();
        }
    }
}