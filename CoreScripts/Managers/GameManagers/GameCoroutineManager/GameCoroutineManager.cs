namespace Managers
{
    public class GameCoroutineManager : IManager
    {
        public static GameCoroutineManager instance;

        public void Awake()
        {
            instance = this;
        }

        public void OnReady()
        {
        }

        public void Setup()
        {
        }

        public void UpdateManager()
        {
        }
    }
}