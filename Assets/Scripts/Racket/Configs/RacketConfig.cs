using Racket.Implementations.Views;
using UnityEngine;

namespace Racket.Configs
{
    /// <summary>
    /// Конфиг ракетки
    /// </summary>
    [CreateAssetMenu(fileName = "RacketConfig", menuName = "Configs/RacketConfig")]
    public class RacketConfig : ScriptableObject
    {
        [SerializeField] private RacketView _racketViewPrefab;
        [SerializeField] private Vector2 _racketSize;
        [SerializeField] private Vector2 _racketPosition;

        /// <summary>
        /// Префаб ракетки
        /// </summary>
        public RacketView RacketViewPrefab => _racketViewPrefab;

        /// <summary>
        /// Размеры ракетки
        /// </summary>
        public Vector2 RacketSize => _racketSize;

        /// <summary>
        /// Позиция
        /// </summary>
        public Vector2 RacketPosition => _racketPosition;
    }
}
