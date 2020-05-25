using Managers;
using System.Collections.Generic;

public class LocalUserInput : AbstractUserInput
{
    private List<LocalUserInputContextData> localUserInputContextDatas;

    public LocalUserInput(AbstractInputController inputController, List<EntityInputLink> entityInputLinks, List<LocalUserInputContextData> localUserInputContextDatas) : base(inputController, entityInputLinks)
    {
        this.localUserInputContextDatas = localUserInputContextDatas;
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);

        GameInputsManager gameInputsManager = inputController.LogicEntity.EntityBlackboard.gameplayController.gameplayManagers.GetManager<GameInputsManager>();

        for (int i = this.localUserInputContextDatas.Count - 1; i >= 0; i--)
            for (int j = this.localUserInputContextDatas[i].abstractEntityInputTriggers.Count - 1; j >= 0; j--)
            {
                AbstractEntityInputTrigger entityInputTrigger = this.localUserInputContextDatas[i].abstractEntityInputTriggers[j];
                gameInputsManager.SubscribeToInput(this.localUserInputContextDatas[i].abstractGameInputTrigger, (GameInputData data) => this.TriggerInput(data, entityInputTrigger));
            }
    }

    public override void UpdateInput()
    {
    }

    public void TriggerInput(GameInputData data, AbstractEntityInputTrigger entityInputTrigger)
    {
        this.entityInputsManager.TriggerInput(entityInputTrigger, new EntityInputData(data));
    }

    public void TriggerInput(GameInputData data, int entityInputTriggerIndex)
    {
        this.entityInputsManager.TriggerInput(entityInputTriggerIndex, new EntityInputData(data));
    }
}