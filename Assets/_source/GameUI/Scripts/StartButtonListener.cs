using DI;
using Utilities;
using Game;

namespace GameUI
{
    internal sealed class StartButtonListener : 
        IGameStartListener
    {
        private Timer _timer;

        private ButtonCollection _startButtonCollection;

        [Inject]
        public void Construct(Timer timer)
        {
            _timer = timer;
            _startButtonCollection = new ButtonCollection(ButtonTypes.Start, timer.gameObject.scene);
            _startButtonCollection.AddAllListeners(StartTimer);
        }

        public float ExecutionPriority => (float)LoadingPriority.Low;

        public void OnGameStart()
        {
            _startButtonCollection.DeactivateAll();
        }

        private void StartTimer()
        {
            _timer.StartTimer();
            _startButtonCollection.RemoveAllListeners(StartTimer);
            _startButtonCollection.DeactivateAll();
        }
    }
}
