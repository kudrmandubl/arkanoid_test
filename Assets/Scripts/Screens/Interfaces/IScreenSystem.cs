using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screens.Interfaces
{
    /// <summary>
    /// Система управления экранами
    /// </summary>
    public interface IScreenSystem
    {
        /// <summary>
        /// Трансформ канваса
        /// </summary>
        Transform CanvasTransform { get; }

        /// <summary>
        /// При создании экрана
        /// </summary>
        Action<IScreen> OnCreateScreen { get; set; }

        /// <summary>
        /// Экземпляры экранов
        /// </summary>
        List<IScreen> ScreenInstances { get; }

        /// <summary>
        /// Инициализировать
        /// </summary>
        /// <param name="canvasKey">Ключ канваса</param>
        void Initialize(string canvasKey);

        /// <summary>
        /// Получить экран
        /// </summary>
        /// <typeparam name="T">Тип экрана</typeparam>
        /// <returns>Экран</returns>
        T GetScreen<T>() where T : IScreen;

        /// <summary>
        /// Получить отображаемый сейчас экран
        /// </summary>
        /// <returns></returns>
        IScreen GetCurrentScreen();

        /// <summary>
        /// Показать экран
        /// </summary>
        /// <typeparam name="T">Тип экрана</typeparam>
        /// <param name="showAsSingle">Показывать единственным</param>
        /// <returns>Показанный экран</returns>
        T ShowScreen<T>(bool showAsSingle = true) where T : IScreen;

        /// <summary>
        /// Скрыть экран
        /// </summary>
        /// <typeparam name="T">Тип экрана</typeparam>
        IScreen HideScreen<T>() where T : IScreen;
    }
}