using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRestrictedTransitioner<T> : AbstractFSMTransitioner<T> where T : Enum
{
    public FSMRestrictedTransitioner(FSMConfig<T> fSMConfig) : base(fSMConfig)
    {
    }

    public override FSMState TransitionateState(T fromState, T toState)
    {
        return AbstractFSMConfig.GetStateFromTransition(fromState, toState);
    }
}
