using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Timer.UI
{
    public class TimerPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _timeText;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetTimeText(int seconds)
        {
            _timeText.text = DataFormatValidator.SecondsToTimeFormat(seconds);
        }
    }
}