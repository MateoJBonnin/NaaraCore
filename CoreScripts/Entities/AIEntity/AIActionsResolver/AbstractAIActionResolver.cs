using System;
using System.Collections.Generic;

public abstract class AbstractAIActionResolver
{
    protected AIBlackboard aIBlackboard;

    public AbstractAIActionResolver(AIBlackboard aIBlackboard)
    {
        this.aIBlackboard = aIBlackboard;
    }

    public List<IAIAction> GetAvailableActions()
    {
        return this.GetActionsBasedOnEntity();
    }

    private List<IAIAction> GetActionsBasedOnEntity()
    {
        Dictionary<Type, List<IAIAction>> aiActionsDatabase = this.FillAIActions();
        List<IAIAction> actionList = new List<IAIAction>();
        foreach (KeyValuePair<ActionRequestType, FSMState> actionStorage in this.aIBlackboard.AILogicEntity.ActionFSM.FSMConfig.statesDatabase)
            actionList.AddRange(aiActionsDatabase[actionStorage.Value.GetType()]);

        return actionList;
    }

    protected abstract Dictionary<Type, List<IAIAction>> FillAIActions();
}