using Managers;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NNetGameClientSynchronizerManager : AbstractGameplayManager
{
    private NetworkingStateLoadManager networkingStateLoadManager;

    private GameInputsManager gameInputsManager;
    private NaaraNetManager naaraNetManager;

    public NNetGameClientSynchronizerManager()
    {
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnReady()
    {
        base.OnReady();
        //NET STATE SNAPSHOT, A DELTA IN WHICH ONLY THE CHANGES SHOULD BE RECORDED
        this.networkingStateLoadManager = new NetworkingStateLoadManager(this.GameplayController.gameplayManagers.GetManager<GameEntityManager>().GetSubManager<ViewGameEntityManager>(), this.GameplayController.gameplayManagers.GetManager<ViewSpawnerManager>());
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetStateUpdatedEvent>(this.OnNNetStateUpdateHandler);
        this.gameInputsManager = this.GameplayController.gameplayManagers.GetManager<GameInputsManager>();
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();

        this.SetupGameInputs();
    }

    private void SetupGameInputs()
    {
        List<AbstractGameInputTrigger> allGameInputs = this.gameInputsManager.gameInputTriggers;

        for (int i = 0; i < allGameInputs.Count; i++)
        {
            int inputIndex = i;
            this.gameInputsManager.SubscribeToInput(allGameInputs[i], (GameInputData gameInputData) => this.OnUserTriggeredGameInput(gameInputData, inputIndex));
        }
    }

    private void OnUserTriggeredGameInput(GameInputData gameInputData, int triggerIndex)
    {
        Debug.LogError("sending input");
        //THE CLIENT SYNC SHOULD BE DONE AT THIS POINT TOO AND WHENEVER THE STATE IS UPDATED IT SHOULD BE SYNCD AGAIN.
        this.naaraNetManager.SendPacketToServer(NetChannelType.Instant, new HLNNetClientGamePacket(NNGamePacketTypeClient.UserInputRequest, new TestingUserInputData(gameInputData, triggerIndex)));
    }

    private void OnNNetStateUpdateHandler(HLNNetStateUpdatedEvent userInputReplyEvent)
    {
        this.networkingStateLoadManager.Load(userInputReplyEvent.networkingStateSnapshot);

        //HAVE A STATE LOADER WHO WILL DO THE FUCK NECESSARY WITH THE STATE THAT WILL BE CONSTRUCTED WITH THIS EVENT DATA
        Debug.LogError("state update");
    }
}

public class TestingUserInputData : INNetGameClientPacketeable
{
    public GameInputData gameInputData;
    public int gameInputIndex;

    public TestingUserInputData()
    {
    }

    public TestingUserInputData(GameInputData gameInputData, int gameInputIndex)
    {
        this.gameInputData = gameInputData;
        this.gameInputIndex = gameInputIndex;
    }

    public void DeserializeNetState(BinaryReader binaryReader)
    {
        this.gameInputIndex = binaryReader.ReadInt32();
        this.gameInputData = new GameInputData(binaryReader.ReadSingle(), binaryReader.ReadSingle());
    }

    public void SerializeNetState(BinaryWriter binaryWriter)
    {
        binaryWriter.Write(gameInputIndex);
        binaryWriter.Write(gameInputData.xAxis);
        binaryWriter.Write(gameInputData.zAxis);
    }
}