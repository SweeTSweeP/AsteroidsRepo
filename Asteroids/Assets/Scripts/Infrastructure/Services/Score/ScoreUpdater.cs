using System;

namespace Infrastructure.Services.Score
{
    public class ScoreUpdater : IScoreUpdater
    {
        private int _currentScore;
        
        public event Action<int> ScoreUpdated;

        public void UpdateScore(EnemyTag tag)
        {
            var score = GetPointsByTag(tag);
            _currentScore += score;
            ScoreUpdated?.Invoke(score);
        }

        public int GetScore() => 
            _currentScore;


        private int GetPointsByTag(EnemyTag enemyTag) =>
            enemyTag switch
            {
                EnemyTag.Asteroid => 200,
                EnemyTag.AsteroidChild => 100,
                EnemyTag.EnemyShip => 300,
                _ => 0
            };
    }
}