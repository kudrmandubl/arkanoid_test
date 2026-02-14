using Common.Utils;
using Racket.Data;
using Racket.Interfaces;
using UnityEngine;

namespace Racket.Implementations.Views
{
    ///  <inheritdoc cref="IRacketView" />
    public class RacketView : CachedMonoBehaviour, IRacketView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        ///  <inheritdoc />
        public BoxCollider2D BoxCollider2D => _boxCollider2D;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public RacketData RacketData { get; set; }

    }
}