using Racket.Interfaces;

namespace Racket.Implementations.Systems
{
    ///  <inheritdoc />
    public class RacketInteractor : IRacketInteractor
    {
        private IRacketView _racketView;

        ///  <inheritdoc />
        public IRacketView RacketView => _racketView;

        /// <summary>
        /// Конструктор
        /// </summary>
        public RacketInteractor()
        {
        }

        ///  <inheritdoc />
        public void SetRacket(IRacketView racketView)
        {
            _racketView = racketView;
        }
    }
}