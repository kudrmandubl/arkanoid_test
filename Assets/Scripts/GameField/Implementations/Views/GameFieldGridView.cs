using System.Collections.Generic;
using Common.Utils;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Views
{
    ///  <inheritdoc cref="IGameFieldGridView" />
    public class GameFieldGridView : CachedMonoBehaviour, IGameFieldGridView
    {
        [SerializeField] private Transform _cellsContainer;

        ///  <inheritdoc />
        public Transform CellsContainer => _cellsContainer;

        ///  <inheritdoc />
        public List<IGameFieldCellView> CellViews { get; set; }
    }
}