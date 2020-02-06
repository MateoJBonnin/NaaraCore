public class NetworkingEventSystemsLoader : AbstractApplicationManager
{
    private LLNetworkingEventSystem lLNetworkingEventSystem;
    private HLNetworkingEventSystem hLNetworkingEventSystem;

    public NetworkingEventSystemsLoader()
    {
        this.lLNetworkingEventSystem = new LLNetworkingEventSystem();
        this.hLNetworkingEventSystem = new HLNetworkingEventSystem();
        this.lLNetworkingEventSystem.Init();
        this.hLNetworkingEventSystem.Init();
    }
}