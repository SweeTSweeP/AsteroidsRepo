using System;
using Infrastructure.Services.ServiceLocator;

namespace Infrastructure.Services.RestartButton
{
    public interface IRestartButtonTrigger : IService
    {
        event Action RestartButtonClicked;
        void RestartSession();
    }
}