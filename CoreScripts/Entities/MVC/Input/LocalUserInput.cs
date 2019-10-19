using Managers;
using System.Collections.Generic;

public class LocalUserInput : AbstractUserInput
{
    public LocalUserInput(AbstractInputController inputController, List<EntityInputLink> entityInputLinks, List<LocalUserInputContextData> localUserInputContextDatas) : base(inputController, entityInputLinks)
    {
        GameInputsManager gameInputsManager = ManagersService.instance.GetManager<GameInputsManager>();

        for (int i = localUserInputContextDatas.Count - 1; i >= 0; i--)
            for (int j = localUserInputContextDatas[i].abstractEntityInputTriggers.Count - 1; j >= 0; j--)
            {
                AbstractEntityInputTrigger entityInputTrigger = localUserInputContextDatas[i].abstractEntityInputTriggers[j];
                gameInputsManager.SubscribeToInput(localUserInputContextDatas[i].abstractGameInputTrigger, (GameInputData data) => TriggerInput(data, entityInputTrigger));
            }
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);
    }

    public override AbstractInputEntityStateSnapshot TempGatherState()
    {
        return new LocalUserInputEntityStateSnapshot() { inputController = this.inputController };
    }

    public override void UpdateInput()
    {
    }

    private void TriggerInput(GameInputData data, AbstractEntityInputTrigger entityInputTrigger)
    {
        this.entityInputsManager.TriggerInput(entityInputTrigger, new EntityInputData(data));
    }
}