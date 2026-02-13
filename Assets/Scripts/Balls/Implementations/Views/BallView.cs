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
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Transform _sizableTransform;
        [SerializeField] private TrailRenderer _trailRenderer;

        ///  <inheritdoc />
        public Action<IBallView, IGameFieldCellView> OnGameFieldCellTriggerEnter { get; set; }

        ///  <inheritdoc />
        public Action<IBallView, IRacketView> OnRacketTriggerEnter { get; set; }

        ///  <inheritdoc />
        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public Transform SizableTransform => _sizableTransform;

        ///  <inheritdoc />
        public TrailRenderer TrailRenderer => _trailRenderer;

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
                OnGameFieldCellTriggerEnter?.Invoke(this, gameFieldCellView);
                return;
            }

            var racketView = collision.GetComponentInParent<IRacketView>();
            if (racketView != null)
            {
                OnRacketTriggerEnter?.Invoke(this, racketView);
                return;
            }
        }
    }
}