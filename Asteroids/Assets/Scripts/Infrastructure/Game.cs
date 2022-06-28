using Infrastructure.Services;
using Infrastructure.Services.Loaders.SceneLoad;
using Infrastructure.States;
using UI;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(Curtain curtain) =>
            _stateMachine = new GameStateMachine(new SceneLoader(), curtain, AllServices.Container);

    }
}