using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAIThinker
{
    public LogicEntity logicEntity;

    public AbstractAIThinker(LogicEntity logicEntity)
    {
        this.logicEntity = logicEntity;
    }

    public abstract List<IAIAction> ResolvePlan(Func<List<IAIAction>> aIActions);
}
