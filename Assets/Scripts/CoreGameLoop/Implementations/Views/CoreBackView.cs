using CoreGameLoop.Interfaces;
using UnityEngine;

namespace CoreGameLoop.Implementations.Views
{
    ///  <inheritdoc cref="ICoreBackView" />
    public class CoreBackView : MonoBehaviour, ICoreBackView
    {
        [SerializeField] private SpriteRenderer _backSpriteRenderer;

        ///  <inheritdoc />
        public SpriteRenderer BackSpriteRenderer => _backSpriteRenderer;
    }
}
