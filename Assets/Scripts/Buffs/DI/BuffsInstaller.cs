using Buffs.Data;
using Buffs.Implementations.Systems;
using Buffs.Implementations.Views;
using Buffs.Interfaces;
using Common.Implementations.Systems.Pools;
using Common.Interfaces;
using Zenject;

namespace Buffs.DI
{
    /// <summary>
    /// DI инстоллер для бафов
    /// </summary>
    public class BuffsInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IMonoBehaviourPool<IBuffView>>().To<MonoBehaviourPool<IBuffView, BuffView>>().AsCached();
            Container.Bind<IPool<BuffData>>().To<SimplePool<BuffData, BuffData>>().AsCached();
            Container.Bind<IBuffSystem>().To<BuffSystem>().AsCached();
        }
    }
}