using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlan
{
    public List<IAIAction> planActions;

    public AIPlan()
    {
        this.planActions = new List<IAIAction>();
    }

    public AIPlan(List<IAIAction> aIActions)
    {
        this.planActions = new List<IAIAction>(aIActions);
    }
}