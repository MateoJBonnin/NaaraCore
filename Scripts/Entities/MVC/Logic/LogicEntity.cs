using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEntity
{
    public AbstractViewEntity ViewEntity { get; set; }
    public EntityStatsConfig EntityStats { get; set; }
    public EntityBlackboard EntityBlackboard { get; set; }
    public GenericFSM<ActionRequestType> ActionFSM { get; set; }

    public LogicEntity(AbstractViewEntity viewEntity, EntityStatsConfig entityStats, GenericFSM<ActionRequestType> ActionFSM)
    {
        this.Init(viewEntity, entityStats, ActionFSM);
        this.EntityBlackboard = new EntityBlackboard(this);
        this.EntityBlackboard.DefaultEntityManagers(new NavManagerConfig());
    }

    public void Init(AbstractViewEntity viewEntity, EntityStatsConfig entityStats, GenericFSM<ActionRequestType> ActionFSM)
    {
        this.ActionFSM = ActionFSM;
        this.EntityStats = entityStats;
        this.ViewEntity = viewEntity;
    }

    public void ProcessAction(ActionRequestType actionRequestType, JSONObject data = null)
    {
        this.ActionFSM.Feed(actionRequestType, data);
    }

    public void Update()
    {
        this.ActionFSM.Update();
        this.EntityBlackboard.UpdateBackboard();
    }
}