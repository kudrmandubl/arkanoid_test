using Balls.Implementations.Views;
using UnityEngine;

namespace Balls.Configs
{
    /// <summary>
    /// Конфиг шариков
    /// </summary>
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Configs/BallsConfig")]
    public class BallsConfig : ScriptableObject
    {
        [SerializeField] private BallView _ballViewPrefab;
        [SerializeField] private Vector2 _ballSize;
        [SerializeField] private float _maxBallStartDirectionX;
        [SerializeField] private float _ballSpeed;
        [SerializeField] private float _extraStartBallPositionY = 0.1f;
        [SerializeField] private float _maxExtraBallSpeed = 5f;
        [SerializeField] private GameFieldCellDestroyParticlesView _gameFieldCellDestroyParticlesPrefab;

        /// <summary>
        /// Префаб шарика
        /// </summary>
        public BallView BallViewPrefab => _ballViewPrefab;

        /// <summary>
        /// Ширина шарика
        /// </summary>
        public Vector2 BallSize => _ballSize;

        /// <summary>
        /// Максимальное начальное направление шарика по X
        /// </summary>
        public float MaxBallStartDirectionX => _maxBallStartDirectionX;

        /// <summary>
        /// Скорость шарика
        /// </summary>
        public float BallSpeed => _ballSpeed;

        /// <summary>
        /// Дополнительная стартовая позиция шарика по Y
        /// </summary>
        public float ExtraStartBallPositionY => _extraStartBallPositionY;

        /// <summary>
        /// Максимальная дополнительная скорость шарика
        /// </summary>
        public float MaxExtraBallSpeed => _maxExtraBallSpeed;

        /// <summary>
        /// Партиклы уничтожения ячейки игрового поля
        /// </summary>
        public GameFieldCellDestroyParticlesView GameFieldCellDestroyParticlesPrefab => _gameFieldCellDestroyParticlesPrefab;
    }
}
