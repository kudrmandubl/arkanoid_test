using Buffs.Data;
using Buffs.Interfaces;
using Common.Utils;
using UnityEngine;

namespace Buffs.Implementations.Views
{
    ///  <inheritdoc cref="IBuffView" />
    public class BuffView : CachedMonoBehaviour, IBuffView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        ///  <inheritdoc />
        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public BuffData BuffData { get; set; }

    }
}