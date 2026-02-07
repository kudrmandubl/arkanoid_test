using Balls.Data;
using Balls.Interfaces;
using Common.Utils;
using UnityEngine;

namespace Balls.Implementations.Views
{
    ///  <inheritdoc cref="IBallView" />
    public class BallView : CachedMonoBehaviour, IBallView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Transform _sizableTransform;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public Transform SizableTransform => _sizableTransform;

        ///  <inheritdoc />
        public BallData BallData { get; set; }

    }
}