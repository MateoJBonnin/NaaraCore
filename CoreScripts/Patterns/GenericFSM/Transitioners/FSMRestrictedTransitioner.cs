using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRestrictedTransitioner<T, W> : AbstractFSMTransitioner<T, W> where T : Enum where W : AbstractFSMData
{
    public FSMRestrictedTransitioner(FSMConfig<T, W> fSMConfig) : base(fSMConfig)
    {
    }

    public override FSMState<W> TransitionateState(T fromState, T toState)
    {
        return AbstractFSMConfig.GetStateFromTransition(fromState, toState);
    }
}
