
namespace Common.Interfaces
{
    /// <summary>
    /// Основная камера
    /// </summary>
    public interface IMainCamera
    {
        /// <summary>
        /// Камера
        /// </summary>
        UnityEngine.Camera Camera { get; }

        /// <summary>
        /// Инициализировать
        /// </summary>
        /// <param name="cameraKey"></param>
        void Initialize(string cameraKey);

        /// <summary>
        /// Сделать камеру перспективной
        /// </summary>
        void SetPerspective();

        /// <summary>
        /// Сделать камеру ортографической
        /// </summary>
        void SetOrthographic();
    }
}
