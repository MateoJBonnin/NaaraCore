using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBlockedTransitioner<T> : AbstractFSMTransitioner<T> where T : Enum
{
    public FSMBlockedTransitioner(FSMConfig<T> fSMConfig) : base(fSMConfig)
    {
    }

    public override FSMState TransitionateState(T fromState, T toState)
    {
        return null;
    }
}
