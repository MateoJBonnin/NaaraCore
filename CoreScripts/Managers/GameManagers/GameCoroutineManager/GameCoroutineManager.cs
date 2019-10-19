namespace Managers
{
    public class GameCoroutineManager : AutoInitManager
    {
        public static GameCoroutineManager instance;

        public void Awake()
        {
            instance = this;
        }
    }
}