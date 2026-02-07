
namespace Balls.Interfaces
{
    /// <summary>
    /// Двигатель шариков
    /// </summary>
    public interface IBallMover
    {
        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value"></param>
        void SetActive(bool value);
    }
}