using Infrastructure.Services.Score;
using Infrastructure.Services.ServiceLocator;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreLabel : MonoBehaviour
    {
        private TMP_Text _label;

        private IScoreUpdater _scoreUpdater;

        private int _currentScore;

        private void Start() => 
            InitLabel();

        private void InitLabel()
        {
            _label = GetComponent<TMP_Text>();
            _scoreUpdater = AllServices.Container.Single<IScoreUpdater>();
            _scoreUpdater.ScoreUpdated += ScoreUpdate;
        }

        private void ScoreUpdate(int score)
        {
            _currentScore += score;
            _label.text = $"Score: {_currentScore}";
        }
    }
}