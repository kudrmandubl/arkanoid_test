using System;
using Balls.Data;
using Balls.Interfaces;
using Common.Utils;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;

namespace Balls.Implementations.Views
{
    ///  <inheritdoc cref="IBallView" />
    public class BallView : CachedMonoBehaviour, IBallView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Transform _sizableTransform;

        ///  <inheritdoc />
        public Action<IBallView, IGameFieldCellView, Collider2D> OnGameFieldCellTriggerEnter { get; set; }

        ///  <inheritdoc />
        public Action<IBallView, IRacketView, Collider2D> OnRacketTriggerEnter { get; set; }

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public Transform SizableTransform => _sizableTransform;

        ///  <inheritdoc />
        public BallData BallData { get; set; }

        /// <summary>
        /// При входе в триггер
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gameFieldCellView = collision.GetComponentInParent<IGameFieldCellView>();
            if (gameFieldCellView != null)
            {
                OnGameFieldCellTriggerEnter?.Invoke(this, gameFieldCellView, collision);
                return;
            }

            var racketView = collision.GetComponentInParent<IRacketView>();
            if (racketView != null)
            {
                OnRacketTriggerEnter?.Invoke(this, racketView, collision);
                return;
            }
        }
    }
}