using Managers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NNetGameClientSynchronizerManager : AbstractGameplayManager
{
    private GameInputsManager gameInputsManager;
    private NaaraNetManager naaraNetManager;

    public NNetGameClientSynchronizerManager()
    {
    }

    public override void OnInit()
    {
        base.OnInit();
        this.gameInputsManager = this.GameplayController.gameplayManagers.GetManager<GameInputsManager>();
        this.naaraNetManager = this.ApplicationController.appManagers.GetManager<NaaraNetManager>();
    }

    public override void OnReady()
    {
        base.OnReady();
        HLNetworkingEventSystem.instance.AddEventListener<HLNNetStateUpdatedEvent>(this.OnNNetStateUpdateHandler);
    }

    private void SetupGameInputs()
    {
        List<AbstractGameInputTrigger> allGameInputs = this.gameInputsManager.gameInputTriggers;

        for (int i = allGameInputs.Count - 1; i >= 0; i--)
        {
            this.gameInputsManager.SubscribeToInput(allGameInputs[i], (GameInputData gameInputData) => this.OnUserTriggeredGameInput(gameInputData, i));
        }
    }

    private void OnUserTriggeredGameInput(GameInputData gameInputData, int triggerIndex)
    {
        Debug.LogError("sending input");
        //THE CLIENT SYNC SHOULD BE DONE AT THIS POINT TOO AND WHENEVER THE STATE IS UPDATED IT SHOULD BE SYNCD AGAIN.
        this.naaraNetManager.SendPacketToServer(NetChannelType.Instant, new HLNNetClientGamePacket(NNGamePacketTypeClient.UserInputRequest, new TestingUserInputData(triggerIndex)));
    }

    private void OnNNetStateUpdateHandler(HLNNetStateUpdatedEvent userInputReplyEvent)
    {
        //HAVE A STATE LOADER WHO WILL DO THE FUCK NECESSARY WITH THE STATE THAT WILL BE CONSTRUCTED WITH THIS EVENT DATA
        Debug.LogError("state update");
    }
}

public class TestingUserInputData : INNetGameClientPacketeable
{
    public int gameInputIndex;

    public TestingUserInputData()
    {
    }

    public TestingUserInputData(int gameInputIndex)
    {
        this.gameInputIndex = gameInputIndex;
    }

    public void DeserializeNetState(BinaryReader binaryReader)
    {
        this.gameInputIndex = binaryReader.ReadInt32();
    }

    public void SerializeNetState(BinaryWriter binaryWriter)
    {
        binaryWriter.Write(gameInputIndex);
    }
}