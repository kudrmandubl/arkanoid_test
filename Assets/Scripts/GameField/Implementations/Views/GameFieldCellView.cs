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

        ///  <inheritdoc />
        public SpriteRenderer MainSpriteRenderer => _mainSpriteRenderer;

        ///  <inheritdoc />
        public GameFieldCellData CellData { get; set; }
    }
}