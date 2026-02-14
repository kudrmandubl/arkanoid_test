using Buffs.Enums;
using UnityEngine;

namespace Buffs.Configs
{
    /// <summary>
    /// Конфиг бафа
    /// </summary>
    [CreateAssetMenu(fileName = "BuffConfig", menuName = "Configs/Buffs/BuffConfig")]
    public class BuffConfig : ScriptableObject
    {
        [SerializeField] private BuffType _buffType;
        [SerializeField] private float _chance;
        [SerializeField] private int _intValue;
        [SerializeField] private float _floatValue;

        /// <summary>
        /// Тип бафа
        /// </summary>
        public BuffType BuffType => _buffType;

        /// <summary>
        /// Шанс
        /// </summary>
        public float Chance => _chance;

        /// <summary>
        /// Инт значение
        /// </summary>
        public int IntValue => _intValue;

        /// <summary>
        /// Флоат значение
        /// </summary>
        public float FloatValue => _floatValue;

    }
}
