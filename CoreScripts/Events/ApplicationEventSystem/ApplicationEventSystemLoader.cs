public class ApplicationEventSystemLoader : AbstractApplicationManager
{
    public ApplicationEventSystem applicationEventSystem;

    public ApplicationEventSystemLoader()
    {
        this.applicationEventSystem = new ApplicationEventSystem();
        this.applicationEventSystem.Init();
    }
}