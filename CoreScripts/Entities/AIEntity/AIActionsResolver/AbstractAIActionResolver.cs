using System;
using System.Collections.Generic;

public abstract class AbstractAIActionResolver
{
    public abstract List<IAIAction> GetAvailableActions();
    //protected abstract GenericDatabaseStructure<FSMState<EntityFSMStateData>, IAIAction> FillAIActions();

    //private List<IAIAction> GetActionsBasedOnEntity()
    //{
    //    GenericDatabaseStructure<FSMState<EntityFSMStateData>, IAIAction> aiActionsDatabase = this.FillAIActions();
    //    List<IAIAction> actionList = new List<IAIAction>();
    //    List<FSMState<EntityFSMStateData>> allActionTypes = this.aIBlackboard.AILogicEntity.EntityBlackboard.subManagerSystem.GetManager<EntityActionProcessorManager>().ActionFSM.FSMStateDatabase.GetAllTypes();
    //
    //    for (int i = allActionTypes.Count - 1; i >= 0; i--)
    //        actionList.AddRange(aiActionsDatabase.GetData(allActionTypes[i]));
    //
    //    return actionList;
    //}
}