public class ApplicationEventSystem : SubscriptionBasedEventSystem<IAppEvent>
{
    public static ApplicationEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}