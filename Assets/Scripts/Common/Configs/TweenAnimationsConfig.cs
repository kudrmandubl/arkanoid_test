using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace Common.Configs
{
    /// <summary>
    /// Конфиг анимаций твинами
    /// </summary>
    [CreateAssetMenu(fileName = "TweenAnimationsConfig", menuName = "Configs/TweenAnimationsConfig")]
    public class TweenAnimationsConfig : ScriptableObject
    {
        /// <summary>
        /// Максимальное количество циклов
        /// </summary>
        public const int MaxLoopsCount = 101;

        [SerializeField] private BaseTweenAnimationConfig[] _animationConfigs;

        private Dictionary<Type, BaseTweenAnimationConfig> _typeToTweenAnimationConfigPairs;

        /// <summary>
        /// Получить конфиг анимации твином
        /// </summary>
        /// <typeparam name="T">Тип конфига анимации твином</typeparam>
        /// <returns>Конфиг анимации твином</returns>
        public T GetTweenAnimationConfig<T>() where T : BaseTweenAnimationConfig
        {
            if (_typeToTweenAnimationConfigPairs == null)
            {
                _typeToTweenAnimationConfigPairs = new Dictionary<Type, BaseTweenAnimationConfig>();
            }

            if (!_typeToTweenAnimationConfigPairs.TryGetValue(typeof(T), out var tweenAnimationConfig))
            {
                tweenAnimationConfig = _animationConfigs.FirstOrDefault(x => x is T);
                _typeToTweenAnimationConfigPairs[typeof(T)] = tweenAnimationConfig;
            }

            return (T) tweenAnimationConfig;
        }

        /// <summary>
        /// Получить конфиг анимации твином
        /// </summary>
        /// <typeparam name="T">Тип конфига анимации твином</typeparam>
        /// <returns>Конфиг анимации твином</returns>
        public T GetTweenAnimationConfig<T>(Func<T, bool> filter) where T : BaseTweenAnimationConfig
        {
            if (_typeToTweenAnimationConfigPairs == null)
            {
                _typeToTweenAnimationConfigPairs = new Dictionary<Type, BaseTweenAnimationConfig>();
            }

            var pair = _typeToTweenAnimationConfigPairs.FirstOrDefault(x => x.Key is T && filter.Invoke(x.Key as T));
            var tweenAnimationConfig = pair.Value;

            if (tweenAnimationConfig == null)
            {
                tweenAnimationConfig = _animationConfigs.FirstOrDefault(x => x is T && filter.Invoke(x as T));
                _typeToTweenAnimationConfigPairs[typeof(T)] = tweenAnimationConfig;
            }

            return (T)tweenAnimationConfig;
        }
    }
}
