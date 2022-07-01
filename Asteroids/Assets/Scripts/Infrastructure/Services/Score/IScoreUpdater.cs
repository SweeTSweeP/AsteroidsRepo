using System;

namespace Infrastructure.Services.Score
{
    public interface IScoreUpdater : IService
    {
        event Action<int> ScoreUpdated;
        void UpdateScore(EnemyTag tag);
        int GetScore();
    }
}