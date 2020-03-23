public class NNetGameServerSynchronizerManager : AbstractGameplayManager
{
    private LogicEntitiesStateSaveManager logicEntitiesSaveManager;
    private NaaraNetManager naaraNetManager;

    public NNetGameServerSynchronizerManager()
    {
    }

    public override void OnInit()
    {
        base.OnInit();
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();
    }

    public override void OnReady()
    {
        base.OnReady();
        this.logicEntitiesSaveManager = new LogicEntitiesStateSaveManager(this.GameplayController.gameplayManagers.GetManager<LogicGameEntityManager>(), this.GameplayController.gameplayManagers.GetManager<LogicSpawnerManager>());
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetUserConnectedToServerSucessfullyEvent>(this.OnRemoteGameplayUserJoinedHandler);
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetUserInputRequestEvent>(this.OnUserInputRequestHandler);
    }

    private void OnRemoteGameplayUserJoinedHandler(HLNNetUserConnectedToServerSucessfullyEvent onRemoteGameplayUserJoinedEvent)
    {
        naaraNetManager.SendPacketToAllUsers(NetChannelType.Instant, new HLNNetServerGamePacket(new HLNNetServerGameStateUpdatePacket()));
    }

    private void OnUserInputRequestHandler(HLNNetUserInputRequestEvent onUserInputRequestHandlerEvent)
    {
        //process input, maybe move unit from map, etc
        //TEMP:

    }
}