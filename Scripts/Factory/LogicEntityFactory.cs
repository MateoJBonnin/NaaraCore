using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEntityFactory
{
    private const string CONNECTIONS_JSON_PATH = "Characters/Connections/";
    private const string ACTIONS_JSON_PATH = "Characters/Actions/";
    private const string GOBLIN_PATH = "Goblin";
    private const string ONI_PATH = "Oni";

    private Dictionary<CharacterType, string> entityPaths;
    private Dictionary<ActionRequestType, Func<FSMState>> entityDefaultTypeToAction;

    public LogicEntityFactory()
    {
        this.entityDefaultTypeToAction = new Dictionary<ActionRequestType, Func<FSMState>>();
        this.entityDefaultTypeToAction[ActionRequestType.Attack] = () => new ACAttack();
        this.entityDefaultTypeToAction[ActionRequestType.Idle] = () => new ACIdle();
        this.entityDefaultTypeToAction[ActionRequestType.Move] = () => new ACMove();
        this.entityDefaultTypeToAction[ActionRequestType.GoTo] = () => new ACMoveTo();

        this.entityPaths = new Dictionary<CharacterType, string>();
        this.entityPaths[CharacterType.Goblin] = GOBLIN_PATH;
        this.entityPaths[CharacterType.Oni] = ONI_PATH;
    }

    public LogicEntity GetLogicEntity(CharacterType characterType, AbstractViewEntity abstractViewEntity)
    {
        return this.CreateLogicEntity(abstractViewEntity, this.entityPaths[characterType]);
    }

    private LogicEntity CreateLogicEntity(AbstractViewEntity abstractViewEntity, string entityPath)
    {
        GenericFSM<ActionRequestType> entityFSM = new GenericFSM<ActionRequestType>();
        LogicEntity logicEntity = new LogicEntity(abstractViewEntity, new EntityStatsConfig(new EPrimaryStats(), new EDerivedStats(), new ESubStats(1, 5f)), entityFSM);
        entityFSM.SetConfig(this.GetEntityConfig(CONNECTIONS_JSON_PATH + entityPath, logicEntity, this.GetEntityDefaultStates(ACTIONS_JSON_PATH + entityPath)));
        entityFSM.SetTransitioner(new FSMRestrictedTransitioner<ActionRequestType>(logicEntity.ActionFSM.FSMConfig));

        return logicEntity;
    }

    private Dictionary<ActionRequestType, FSMState> GetEntityDefaultStates(string entityActionsDataPath)
    {
        JSONObject data = JSONObject.Create(Resources.Load(entityActionsDataPath).ToString());
        Dictionary<ActionRequestType, FSMState> entityActions = new Dictionary<ActionRequestType, FSMState>();

        foreach (var item in data.list)
        {
            ActionRequestType action = FSMConfig<ActionRequestType>.stringNameToT(item.str);
            entityActions[action] = this.entityDefaultTypeToAction[action]();
        }

        return entityActions;
    }

    private EntityFSMConfig GetEntityConfig(string charDataPath, LogicEntity logicEntity, Dictionary<ActionRequestType, FSMState> statesData)
    {
        JSONObject data = JSONObject.Create(Resources.Load(charDataPath).ToString());
        return new EntityFSMConfig(logicEntity, data, statesData);
    }
}