public class AutoInitManager : DefaultManager
{
    public override void Setup()
    {
        base.Setup();
        this.subManagerStateFSM.Feed(ManagerReadyStates.Ready);
    }
}