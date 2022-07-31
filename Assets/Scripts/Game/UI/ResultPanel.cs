using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _replayButton;

        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private Text _resultText;

        public event Action OnReplayButtonClicked;
        public event Action OnQuitButtonClicked;

        private const string _winMessage = "YOU WIN";
        private const string _loseMessage = "YOU LOSE";

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetWinnerText(bool isWin)
        {
            _resultText.text = isWin ? _winMessage : _loseMessage;
        }

        private void Awake()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        private void OnDestroy()
        {
            _replayButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        private void OnReplayButtonClick()
        {
            OnReplayButtonClicked?.Invoke();
        }

        private void OnQuitButtonClick()
        {
            OnQuitButtonClicked?.Invoke();
        }
    }
}