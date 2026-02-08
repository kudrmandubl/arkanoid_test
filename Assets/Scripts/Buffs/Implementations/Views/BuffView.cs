using System;
using Buffs.Data;
using Buffs.Interfaces;
using Common.Utils;
using Racket.Interfaces;
using UnityEngine;

namespace Buffs.Implementations.Views
{
    ///  <inheritdoc cref="IBuffView" />
    public class BuffView : CachedMonoBehaviour, IBuffView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BuffViewVariant[] _buffViewVariants;

        ///  <inheritdoc />
        public Action<IBuffView, IRacketView> OnRacketTriggerEnter { get; set; }

        ///  <inheritdoc />
        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public BuffViewVariant[] BuffViewVariants => _buffViewVariants;

        ///  <inheritdoc />
        public BuffData BuffData { get; set; }

        /// <summary>
        /// При входе в триггер
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var racketView = collision.GetComponentInParent<IRacketView>();
            if (racketView != null)
            {
                OnRacketTriggerEnter?.Invoke(this, racketView);
                return;
            }
        }
    }
}