using Infrastructure.Services.Player;
using Infrastructure.Services.Score;
using Infrastructure.Services.ServiceLocator;
using TMPro;
using UnityEngine;

namespace UI.EndGameScreen
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text resultLabel;
        
        private CanvasGroup _canvasGroup;

        private IPlayerDeathIndicator _playerDeathIndicator;
        private IScoreUpdater _scoreUpdater;
        
        private void Start() => 
            InitGameOverScreen();

        private void InitGameOverScreen()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _scoreUpdater = AllServices.Container.Single<IScoreUpdater>();
            _playerDeathIndicator = AllServices.Container.Single<IPlayerDeathIndicator>();
            _playerDeathIndicator.PlayerDied += ShowGameOverScreen;
            HideGameOverScreen();
        }

        private void ShowGameOverScreen()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            resultLabel.text = $"Your Score: {_scoreUpdater.GetScore()}";
        }

        public void HideGameOverScreen()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
        }
    }
}