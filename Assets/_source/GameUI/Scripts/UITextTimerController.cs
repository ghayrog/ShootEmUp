using System.Globalization;
using UnityEngine;
using DI;
using Utilities;

namespace GameUI
{
    //[RequireComponent(typeof(GameText))]
    internal sealed class UITextTimerController : MonoBehaviour
    {
        private Timer _timer;

        private GameText _gameText;

        [Inject]
        public void Construct(GameText gameText, Timer timer)
        {
            Debug.Log("Construct UITextTimerController");
            _timer = timer;
            _gameText = gameText;
        }

        private void Start()
        {
            //_gameText= GetComponent<GameText>();

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
            _gameText.gameObject.SetActive(false);
            _timer.OnTimerCompleted -= DisableTextTimerController;
        }
    }
}
