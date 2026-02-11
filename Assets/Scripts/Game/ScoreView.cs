using UnityEngine;
using Flappy.Core;
using Zenject;
using TMPro;

namespace Flappy.Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _recordScore;
        [SerializeField] private TMP_Text _totalScore;
        [SerializeField] private TMP_Text _averageScore;
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(UpdateTexts);
        }
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<ScoreChangedSignal>(UpdateTexts);
        }

        private void UpdateTexts(ScoreChangedSignal signal)
        {
            _currentScore.text = "Score:" + signal.CurrentScore.ToString();
            _recordScore.text = "Record:" + signal.RecordScore.ToString();
            _totalScore.text = "Total:" + signal.TotalScore.ToString();
            _averageScore.text = "Average:" + signal.AverageScore.ToString("F3");
        }
    }
}