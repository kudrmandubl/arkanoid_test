using System.Collections.Generic;
using Balls.Interfaces;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallInteractor : IBallInteractor
    {
        private List<IBallView> _ballViews;

        ///  <inheritdoc />
        public List<IBallView> BallViews => _ballViews;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallInteractor()
        {
            _ballViews = new List<IBallView>();
        }

        ///  <inheritdoc />
        public void AddBall(IBallView ballView)
        {
            _ballViews.Add(ballView);
        }

        ///  <inheritdoc />
        public void RemoveBall(IBallView ballView)
        {
            _ballViews.Remove(ballView);
        }
    }
}