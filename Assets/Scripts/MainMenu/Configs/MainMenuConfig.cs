using UnityEngine;

namespace MainMenu.Configs
{
    /// <summary>
    /// Конфиг основного меню
    /// </summary>
    [CreateAssetMenu(fileName = "MainMenuConfig", menuName = "Configs/MainMenu/MainMenuConfig")]
    public class MainMenuConfig : ScriptableObject
    {
        [SerializeField] private float _frontMaxParallaxOffset = 1f;
        [SerializeField] private float _backMaxParallaxOffset = 0.25f;
        [SerializeField] private float _parallaxSpeed = 0.3f;

        /// <summary>
        /// Максимальный оффест параллакса переднего плана
        /// </summary>
        public float FrontMaxParallaxOffset => _frontMaxParallaxOffset;

        /// <summary>
        /// Максимальный оффекс параллакса для фона
        /// </summary>
        public float BackMaxParallaxOffset => _backMaxParallaxOffset;

        /// <summary>
        /// Скорость параллакса
        /// </summary>
        public float ParallaxSpeed => _parallaxSpeed;
    }
}
