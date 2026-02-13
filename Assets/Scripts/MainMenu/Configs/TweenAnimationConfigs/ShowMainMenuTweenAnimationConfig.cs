using Common.Configs;
using UnityEngine;

namespace MainMenu.Configs
{
    /// <summary>
    /// Конфиг анимации показа основного меню
    /// </summary>
    [CreateAssetMenu(fileName = "ShowMainMenuTweenAnimationConfig", menuName = "Configs/MainMenu/TweenAnimationConfigs/ShowMainMenuTweenAnimationConfig")]
    public class ShowMainMenuTweenAnimationConfig : BaseTweenAnimationConfig
    {
        [SerializeField] private float _duration = 1f;

        /// <summary>
        /// Продолжительность
        /// </summary>
        public float Duration => _duration;
    }
}
