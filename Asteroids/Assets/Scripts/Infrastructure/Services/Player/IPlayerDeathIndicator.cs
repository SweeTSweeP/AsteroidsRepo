using System;

namespace Infrastructure.Services.Player
{
    public interface IPlayerDeathIndicator : IService
    {
        event Action PlayerDied;
        void PlayerDie();
    }
}