using System;

namespace Infrastructure.Services.RestartButton
{
    public interface IRestartButtonTrigger : IService
    {
        event Action RestartButtonClicked;
        void RestartSession();
    }
}