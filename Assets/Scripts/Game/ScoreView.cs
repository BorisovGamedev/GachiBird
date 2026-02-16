using System;
using UnityEngine;
using TMPro;
using Zenject;

namespace Flappy.Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _recordScore;
        [SerializeField] private TMP_Text _totalScore;

        private ScoreProvider _scoreProvider;

        [Inject]
        public void Init(ScoreProvider scoreProvider)
        {
            _scoreProvider = scoreProvider;
        }

        private void OnEnable()
        {
            _scoreProvider.ValuesChanged += UpdateTexts;
        }

        public void UpdateTexts()
        {
            _currentScore.text = "Score:" + _scoreProvider.CurrentScore;
            _recordScore.text = "Record:" + _scoreProvider.RecordScore;
            _totalScore.text = "Total:" + _scoreProvider.TotalScore;
        }
    }
}