using System;

namespace Infrastructure.Services.RestartButton
{
    public class RestartButtonTrigger : IRestartButtonTrigger
    {
        public event Action RestartButtonClicked;
        
        public void RestartSession() => 
            RestartButtonClicked?.Invoke();
    }
}