using System.Globalization;
using UnityEngine;
using Utilities;

namespace Game
{
    [RequireComponent(typeof(GameText))]
    internal sealed class UITextTimerController : MonoBehaviour
    {
        [SerializeField]
        private Timer _timer;

        private GameText _gameText;

        private void Awake()
        {
            _gameText= GetComponent<GameText>();

            _timer.OnTimerStarted += SetTextByNumber;
            _timer.OnTimerChanged += SetTextByNumber;
            _timer.OnTimerCompleted += DisableTextTimerController;
        }

        private void OnDestroy()
        {
            _timer.OnTimerStarted -= SetTextByNumber;
            _timer.OnTimerChanged -= SetTextByNumber;
            _timer.OnTimerCompleted -= DisableTextTimerController;
        }

        private void SetTextByNumber(int counter, float timeStep)
        {
            _gameText.ShowGameMessage(counter.ToString(CultureInfo.InvariantCulture), timeStep/2);
        }

        private void DisableTextTimerController()
        { 
            gameObject.SetActive(false);
            _timer.OnTimerCompleted -= DisableTextTimerController;
        }
    }
}
