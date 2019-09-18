using System.Collections.Generic;
using System.Linq;

public class LogicEntityFactory
{
    public LogicEntity GetLogicEntity(EntityLogicBlueprintScriptable logicbluePrint, AbstractViewEntity abstractViewEntity)
    {
        return this.CreateLogicEntity(abstractViewEntity, logicbluePrint);
    }

    private LogicEntity CreateLogicEntity(AbstractViewEntity abstractViewEntity, EntityLogicBlueprintScriptable logicScriptable)
    {
        //TODO: FIX LOGIC ENTITY STATS
        LogicEntity logicEntity = new LogicEntity(abstractViewEntity);//, new EntityStatsConfig(new EPrimaryStats(), new EDerivedStats(), new ESubStats(1, 5f)));
        for (int i = logicScriptable.entityManagersList.entityManagersList.Count - 1; i >= 0; i--)
        {
            EntityManagerContextConfigurableScriptable entityManagerScriptable = logicScriptable.entityManagersList.entityManagersList[i];
            entityManagerScriptable.Configure();
            entityManagerScriptable.EntityManager.ConfigureEntity(logicEntity);
            logicEntity.EntityBlackboard.subManagerSystem.RegisterSubManager(entityManagerScriptable.EntityManager);
        }

        //TODO: FIX THIS DO NOT CONFIG, THIS IS FOR THE MANAGER, HERE YOU SHOULD CONFIGURE ALL THE ENTITY MANAGERS AND THEN EVERY MANAGER SHOULD BE ABLE T OCONFIGURE
        GenericFSM<ActionRequestType, EntityFSMStateData> entityFSM = new GenericFSM<ActionRequestType, EntityFSMStateData>();

        EntityFSMDatabase entityFSMConfig = this.GetEntityConfig(logicEntity, logicScriptable, this.GetEntityDefaultStates(logicScriptable));
        entityFSM.SetConfig(entityFSMConfig);
        entityFSM.SetFSMTransitioner(new FSMRestrictedTransitioner<ActionRequestType, EntityFSMStateData>(entityFSMConfig));

        return logicEntity;
    }

    private Dictionary<ActionRequestType, FSMState<EntityFSMStateData>> GetEntityDefaultStates(EntityLogicBlueprintScriptable logicScriptable)
    {
        Dictionary<ActionRequestType, FSMState<EntityFSMStateData>> data = new Dictionary<ActionRequestType, FSMState<EntityFSMStateData>>();

        for (int i = logicScriptable.entityActionList.entityActionScriptables.Count - 1; i >= 0; i--)
        {
            EntityActionScriptable action = logicScriptable.entityActionList.entityActionScriptables[i];
            data[action.ActionRequestType] = action.ActionState;
        }

        return data;
    }

    private EntityFSMDatabase GetEntityConfig(LogicEntity logicEntity, EntityLogicBlueprintScriptable logicScriptable, Dictionary<ActionRequestType, FSMState<EntityFSMStateData>> statesData)
    {
        List<FSMStateLink<ActionRequestType>> logicFSMStateLinks = new List<FSMStateLink<ActionRequestType>>();

        for (int i = logicScriptable.entityActionFSMLinkDataList.entityActionFSMLinkData.Count - 1; i >= 0; i--)
        {
            ActionRequestType actionType = logicScriptable.entityActionFSMLinkDataList.entityActionFSMLinkData[i].entityActionScriptable.ActionRequestType;
            for (int j = logicScriptable.entityActionFSMLinkDataList.entityActionFSMLinkData[i].linkedActions.Count - 1; i >= 0; i--)
            {
                ActionRequestType linkedActionType = logicScriptable.entityActionFSMLinkDataList.entityActionFSMLinkData[i].linkedActions[j].ActionRequestType;
                logicFSMStateLinks.Add(new FSMStateLink<ActionRequestType>(actionType, linkedActionType));
            }
        }

        FSMStateLinksData<ActionRequestType> data = new FSMStateLinksData<ActionRequestType>(logicFSMStateLinks);
        return new EntityFSMDatabase(logicEntity, data, statesData);
    }
}