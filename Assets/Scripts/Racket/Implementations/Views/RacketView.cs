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
        [SerializeField] private Transform _sizableTransform;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public Transform SizableTransform => _sizableTransform;

        ///  <inheritdoc />
        public RacketData RacketData { get; set; }

    }
}