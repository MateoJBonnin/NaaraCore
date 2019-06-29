public class GameEventSystem : SubscriptionBasedEventSystem<IGameEvent>
{
    public static GameEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}