using Cysharp.Threading.Tasks;

namespace MenuLoop.Interfaces
{
    /// <summary>
    /// Система цикла меню
    /// </summary>
    public interface IMenuLoopSystem
    {
        /// <summary>
        /// Начать цикл меню
        /// </summary>
        UniTask StartMenuLoopAsync();
    }
}