using Common.Configs;
using UnityEngine;

namespace MainMenu.Configs
{
    /// <summary>
    /// Конфиг анимации начала кор игры
    /// </summary>
    [CreateAssetMenu(fileName = "StartCoreGameTweenAnimationConfig", menuName = "Configs/MainMenu/TweenAnimationConfigs/StartCoreGameTweenAnimationConfig")]
    public class StartCoreGameTweenAnimationConfig : BaseTweenAnimationConfig
    {
        [SerializeField] private float _duration = 1f;

        /// <summary>
        /// Продолжительность
        /// </summary>
        public float Duration => _duration;
    }
}
