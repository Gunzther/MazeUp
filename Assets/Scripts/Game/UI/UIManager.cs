using Timer.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private TimerPanel _timerPanel;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private ResultPanel _resultPanel;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _resultPanel.OnReplayButtonClicked += OnReplayButtonClick;
            _resultPanel.OnQuitButtonClicked += OnQuitButtonClick;
            _gameManager.OnGameEnd += OnGameEnd;
        }

        private void Start()
        {
            _gameManager.Timer.OnTimeUpdated += UpdateTime;
        }

        private void OnDestroy()
        {
            _gameManager.Timer.OnTimeUpdated -= UpdateTime;
            _resultPanel.OnReplayButtonClicked -= OnReplayButtonClick;
            _resultPanel.OnQuitButtonClicked -= OnQuitButtonClick;
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnStartButtonClick()
        {
            _startButton.gameObject.SetActive(false);
            _timerPanel.SetActive(true);
            _gameManager.StartGame();
        }

        private void OnReplayButtonClick()
        {
            _resultPanel.SetActive(false);
            _gameManager.ResetGame();
            _gameManager.StartGame();
        }

        private void OnQuitButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void UpdateTime(int time)
        {
            _timerPanel.SetTimeText(time);
        }

        private void OnGameEnd(bool isWin)
        {
            _resultPanel.SetWinnerText(isWin);
            _resultPanel.SetActive(true);
        }
    }
}