using Infrastructure.States;
using UI.Curtain;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Curtain curtain;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(curtain);
            _game._stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}