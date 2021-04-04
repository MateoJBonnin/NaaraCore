public class GameplayEventSystem : SubscriptionBasedEventSystem<IGameEvent>
{
    public static GameplayEventSystem Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
