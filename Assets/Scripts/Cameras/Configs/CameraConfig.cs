using UnityEngine;

namespace Cameras.Configs
{
    /// <summary>
    /// Конфиг камеры
    /// </summary>
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [SerializeField] private float _aspectRatioWidth = 16f;
        [SerializeField] private float _aspectRatioHeight = 9f;
        // TODO: на данный момоент можно оставить каждый кадр - на будущее логично сделать на событие изменения размера экрана
        [SerializeField] private bool _updateEveryFrame = true;

        /// <summary>
        /// Ширина в соотношении стороны
        /// </summary>
        public float AspectRatioWidth => _aspectRatioWidth;

        /// <summary>
        /// Высота в соотношении стороны
        /// </summary>
        public float AspectRatioHeight => _aspectRatioHeight;

        /// <summary>
        /// Флаг необходимости обновления каждый кадр
        /// </summary>
        public bool UpdateEveryFrame => _updateEveryFrame;
    }
}
