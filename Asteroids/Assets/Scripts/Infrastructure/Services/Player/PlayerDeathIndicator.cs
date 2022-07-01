using System;

namespace Infrastructure.Services.Player
{
    public class PlayerDeathIndicator : IPlayerDeathIndicator
    {
        public event Action PlayerDied;
        
        public void PlayerDie() => 
            PlayerDied?.Invoke();
    }
}