using Common.Utils;
using GameField.Data;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Views
{
    ///  <inheritdoc cref="IGameFieldCellView" />
    public class GameFieldCellView : CachedMonoBehaviour, IGameFieldCellView
    {
        [SerializeField] private SpriteRenderer _mainSpriteRenderer;
        [SerializeField] private Transform _sizableTransform;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private GameObject _explosiveRoot;

        ///  <inheritdoc />
        public Collider2D Collider2D => _collider2D;

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public Transform SizableTransform => _sizableTransform;

        ///  <inheritdoc />
        public GameObject ExplosiveRoot => _explosiveRoot;

        ///  <inheritdoc />
        public GameFieldCellData CellData { get; set; }
    }
}