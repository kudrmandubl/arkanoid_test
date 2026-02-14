using GameField.Implementations.Views;
using UnityEngine;

namespace GameField.Configs
{
    /// <summary>
    /// Конфиг игрового поля
    /// </summary>
    [CreateAssetMenu(fileName = "GameFieldConfig", menuName = "Configs/GameFieldConfig")]
    public class GameFieldConfig : ScriptableObject
    {
        [SerializeField] private GameFieldCellView _gameFieldCellViewPrefab;
        [SerializeField] private GameFieldGridView _gameFieldGridViewPrefab;
        [SerializeField] private Vector2Int _gameFieldSize;
        [SerializeField] private Vector2 _gameFieldCellSize;
        [SerializeField] private float _gameFieldCellPadding = 0.02f;
        [SerializeField] private Vector2 _gameFieldGridPosition;
        [SerializeField] private GameFieldCellDestroyParticlesView _gameFieldCellDestroyParticlesPrefab;
        [SerializeField] private GameFieldCellExplosionParticlesView _gameFieldCellExplosionParticlesPrefab;

        /// <summary>
        /// Префаб ячейки игрового поля
        /// </summary>
        public GameFieldCellView GameFieldCellViewPrefab => _gameFieldCellViewPrefab;

        /// <summary>
        /// Префаб сетки игрового поля
        /// </summary>
        public GameFieldGridView GameFieldGridViewPrefab => _gameFieldGridViewPrefab;
    
        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public Vector2Int GameFieldSize => _gameFieldSize;

        /// <summary>
        /// Размер ячейки игрового поля
        /// </summary>
        public Vector2 GameFieldCellSize => _gameFieldCellSize;

        /// <summary>
        /// Отступ ячейки игрового поля
        /// </summary>
        public float GameFieldCellPadding => _gameFieldCellPadding;

        /// <summary>
        /// Позиция игровой сетки
        /// </summary>
        public Vector2 GameFieldGridPosition => _gameFieldGridPosition;

        /// <summary>
        /// Партиклы уничтожения ячейки игрового поля
        /// </summary>
        public GameFieldCellDestroyParticlesView GameFieldCellDestroyParticlesPrefab => _gameFieldCellDestroyParticlesPrefab;

        /// <summary>
        /// Партиклы уничтожения ячейки игрового поля
        /// </summary>
        public GameFieldCellExplosionParticlesView GameFieldCellExplosionParticlesPrefab => _gameFieldCellExplosionParticlesPrefab;
    }
}
