namespace Game
{

    public interface IGameListener
    {
        float Priority { get; }
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    }

    public interface IGamePauseListener : IGameListener
    { 
        void OnGamePause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnGameResume();
    }

    public interface IGameUpdateListener : IGameListener
    { 
        void OnUpdate();
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate();
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate();
    }

}
