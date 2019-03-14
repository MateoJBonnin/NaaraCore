using System;
using UnityEngine;

public abstract class AbstractAIPatrolBehaviour
{
    public Action OnPatrolSpotReached;
    public Action OnPatrolInterrumped;

    public abstract void RunPatrolAction();
    public virtual void UpdatePatrolBehaviour()
    {
        this.getCloseAction.UpdateAction();
    }

    protected AIGetCloseTargetAction getCloseAction;
    protected LogicEntity logicEntity;

    public AbstractAIPatrolBehaviour(LogicEntity logicEntity)
    {
        this.logicEntity = logicEntity;
        this.getCloseAction = new AIGetCloseTargetAction(this.logicEntity);
    }
}