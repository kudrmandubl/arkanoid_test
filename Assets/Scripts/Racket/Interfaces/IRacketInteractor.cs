
namespace Racket.Interfaces
{
    /// <summary>
    /// Точка взаимодействия с ракеткой
    /// </summary>
    public interface IRacketInteractor
    {
        /// <summary>
        /// Отображение ракетки
        /// </summary>
        IRacketView RacketView { get; }

        /// <summary>
        /// Установить ракетку
        /// </summary>
        void SetRacket(IRacketView racketView);
    }
}