using Buffs.Implementations.Views;
using UnityEngine;

namespace Buffs.Configs
{
    /// <summary>
    /// Конфиг бафов
    /// </summary>
    [CreateAssetMenu(fileName = "BuffsConfig", menuName = "Configs/Buffs/BuffsConfig")]
    public class BuffsConfig : ScriptableObject
    {
        [SerializeField] private BuffView _buffViewPrefab;
        [SerializeField] private BuffConfig[] _buffConfigs;
        [SerializeField] private float _buffChance = 0.5f;
        [SerializeField] private float _buffSpeed = 1;

        /// <summary>
        /// Префаб отображения бафа
        /// </summary>
        public BuffView BuffViewPrefab => _buffViewPrefab;

        /// <summary>
        /// Конфиги бафов
        /// </summary>
        public BuffConfig[] BuffConfigs => _buffConfigs;

        /// <summary>
        /// Шанс выпадения бафа
        /// </summary>
        public float BuffChance => _buffChance;

        /// <summary>
        /// Скорость бафа
        /// </summary>
        public float BuffSpeed => _buffSpeed;
    }
}
