using Infrastructure.Services;
using Infrastructure.Services.RestartButton;
using UI.EndGameScreen;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RestartButton
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private GameOverScreen gameOverScreen;
        
        private Button _restartButton;

        private IRestartButtonTrigger _restartButtonTrigger;
        
        private void Start()
        {
            _restartButton = GetComponent<Button>();
            _restartButtonTrigger = AllServices.Container.Single<IRestartButtonTrigger>();
            AddRestartListener();
        }

        private void AddRestartListener() => 
            _restartButton.onClick.AddListener(() =>
            {
                gameOverScreen.HideGameOverScreen();
                _restartButtonTrigger.RestartSession();
            });
    }
}