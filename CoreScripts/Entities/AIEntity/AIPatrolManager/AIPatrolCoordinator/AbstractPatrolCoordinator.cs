using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPatrolCoordinator
{
    protected LogicEntity logicEntity;

    public AbstractPatrolCoordinator(LogicEntity logicEntity)
    {
        this.logicEntity = logicEntity;
    }

    public abstract int GetNextPatrolPosition(int currentPositionIndex);
}