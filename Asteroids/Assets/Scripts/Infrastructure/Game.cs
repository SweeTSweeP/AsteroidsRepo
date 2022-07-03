using Infrastructure.Services.Loaders.SceneLoad;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.States;
using UI.Curtain;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(Curtain curtain) =>
            _stateMachine = new GameStateMachine(new SceneLoader(), curtain, AllServices.Container);

    }
}