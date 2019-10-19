using System.Collections.Generic;

public abstract class AbstractInputEntity
{
    protected AbstractInputController inputController;
    protected EntityInputsManager entityInputsManager;

    protected AbstractInputEntity()
    {
    }

    public AbstractInputEntity(AbstractInputController inputController, List<EntityInputLink> entityInputLinks)
    {
        this.entityInputsManager = new EntityInputsManager();
        this.inputController = inputController;

        for (int i = entityInputLinks.Count - 1; i >= 0; i--)
        {
            ActionFSMState state = entityInputLinks[i].actionFSMState;
            this.entityInputsManager.SubscribeToInput(entityInputLinks[i].entityInput, (EntityInputData entityInputData) => this.ProcessEntityInput(entityInputData, state));
        }
    }

    public virtual void SetLogic(LogicEntity logicEntity)
    {
        this.inputController.SetLogicToControl(logicEntity);
    }

    //TEMP, FIX ASAP ALSO THINK TO MAKE THE INPUT ENTITIES FROM HIERARCHY TO COMPONENTS, ALSO MAKE IT NON MONOBEHAVIOUR SINCE
    // WE HAVE INPUT SERVICE
    public abstract AbstractInputEntityStateSnapshot TempGatherState();
    public abstract void UpdateInput();

    public virtual void ProcessEntityInput(EntityInputData data, ActionFSMState state)
    {
        GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(state, this.inputController.LogicEntity, data));
    }
}