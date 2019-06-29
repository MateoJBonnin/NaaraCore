using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMForcedTransitioner<T> : AbstractFSMTransitioner<T> where T : Enum
{
    public FSMForcedTransitioner(FSMConfig<T> fSMConfig) : base(fSMConfig)
    {
    }

    public override FSMState TransitionateState(T fromState, T toState)
    {
        return this.AbstractFSMConfig.GetState(toState);
    }
}
