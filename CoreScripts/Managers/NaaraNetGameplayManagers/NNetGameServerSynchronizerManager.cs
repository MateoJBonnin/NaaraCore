using Managers;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNetGameServerSynchronizerManager : AbstractGameplayManager
{
    private NetworkingStateSaveManager networkingStateSaveManager;
    private NaaraNetManager naaraNetManager;


    //TODO THIS SHOULDNT BE HERE
    private EntityBlueprintScriptable entityBlueprint;
    //TODO: TEMP THIS SHOULD BE SOMETHING THAT SPAWN THAT
    private ViewSpawnerManager viewSpawnerManager;
    private LogicSpawnerManager logicSpawnerManager;
    private InputSpawnerManager inputSpawnerManager;

    //TODO THIS SHOULDNT BE HERE
    public NNetGameServerSynchronizerManager(EntityBlueprintScriptable entityBlueprint)
    {
        this.entityBlueprint = entityBlueprint;
    }

    public override void OnInit()
    {
        base.OnInit();
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();
    }

    public override void OnReady()
    {
        base.OnReady();

        this.viewSpawnerManager = this.GameplayController.gameplayManagers.GetManager<ViewSpawnerManager>();
        this.logicSpawnerManager = this.GameplayController.gameplayManagers.GetManager<LogicSpawnerManager>();
        this.inputSpawnerManager = this.GameplayController.gameplayManagers.GetManager<InputSpawnerManager>();

        var asd1 = this.GameplayController.gameplayManagers.GetManager<GameEntityManager>();
        var asd2 = asd1.GetSubManager<ViewGameEntityManager>();
        this.networkingStateSaveManager = new NetworkingStateSaveManager(this.GameplayController.gameplayManagers.GetManager<GameEntityManager>().GetSubManager<ViewGameEntityManager>(), this.GameplayController.gameplayManagers.GetManager<ViewSpawnerManager>());
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetUserConnectedToServerSucessfullyEvent>(this.OnRemoteGameplayUserJoinedHandler);
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetUserInputRequestEvent>(this.OnUserInputRequestHandler);

        //ESTA CLASE DEBERIA ESTAR ENCARGADA DE PRODUCIR UN NETSTATESNAPTSHOT QUE SEA UN DELTA CON TODOS LOS CAMBIOS DE LOS USUARIOS Y QUE SE ENVIEN CADA(0.1)n SEGS A TODOS COMO UN UPDATE
        //    Y QUE SOLO SE UPDATEE O SE AVISE DEL UPDATE DE MODIFICACIONES
    }

    //TODO: FIX THIS, I AM ONLY DOING THIS TO QUICKLY MAKE THE CLIENT CHARACTER MOVE
    AbstractInputEntity inputEntity;

    private void OnRemoteGameplayUserJoinedHandler(HLNNetUserConnectedToServerSucessfullyEvent onRemoteGameplayUserJoinedEvent)
    {
        //TODO: TEEEMP
        //TODO: CHECK WHERE SHOULD THIS BE
        this.GameplayController.gameplayManagers.GetManager<ViewSpawnerManager>();

        ViewEntity viewMainCharacter = this.viewSpawnerManager.SpawnCharacter(this.entityBlueprint.entityViewBlueprint);
        LogicEntity logicEntity = this.logicSpawnerManager.BuildLogic(this.entityBlueprint.entityLogicBlueprint);
        inputEntity = this.inputSpawnerManager.BuildInput(this.entityBlueprint.entityInputBlueprint.abstractInputEntityScriptable.AbstractInputEntity, logicEntity);//viewMainCharacter.gameObject.AddComponent<InputToLogicController>();

        Timing.RunCoroutine(testing());

        //naaraNetManager.SendPacketToAllUsers(NetChannelType.Instant, new HLNNetServerGamePacket(new HLNNetServerGameStateUpdatePacket(this.networkingStateSaveManager.GetStateSnapshot())));
    }

    private IEnumerator<float> testing()
    {
        yield return Timing.WaitForSeconds(3);
        naaraNetManager.SendPacketToAllUsers(NetChannelType.Instant, new HLNNetServerGamePacket(new HLNNetServerGameStateUpdatePacket(this.networkingStateSaveManager.GetStateSnapshot())));
    }

    private void OnUserInputRequestHandler(HLNNetUserInputRequestEvent onUserInputRequestHandlerEvent)
    {
        //process input, maybe move unit from map, etc
        //TEMP:
        //TODO: Quiza tenga que hacer un entity blue print manager que te de las blueprint basados en algun index
        LocalUserInput localUser = inputEntity as LocalUserInput;

        GameInputsManager gameInputsManager = GameplayController.gameplayManagers.GetManager<GameInputsManager>();
        gameInputsManager.TriggerInput(onUserInputRequestHandlerEvent.gameInputIndex, onUserInputRequestHandlerEvent.gameInputData);
        //localUser.TriggerInput(onUserInputRequestHandlerEvent.gameInputData, onUserInputRequestHandlerEvent.gameInputIndex);
        naaraNetManager.SendPacketToAllUsers(NetChannelType.Instant, new HLNNetServerGamePacket(new HLNNetServerGameStateUpdatePacket(this.networkingStateSaveManager.GetStateSnapshot())));
    }
}