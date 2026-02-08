using Buffs.Enums;
using UnityEngine;

namespace Buffs.Implementations.Views
{
    /// <summary>
    /// Вариант отображения бафа
    /// </summary>
    public class BuffViewVariant : MonoBehaviour
    {
        [SerializeField] private BuffType _buffType;

        ///  <inheritdoc />
        public BuffType BuffType => _buffType;
    }
}