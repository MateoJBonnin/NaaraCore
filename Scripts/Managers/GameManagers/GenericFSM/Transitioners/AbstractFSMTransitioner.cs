using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFSMTransitioner<T> where T : Enum
{
    protected FSMConfig<T> AbstractFSMConfig { get; set; }

    public AbstractFSMTransitioner(FSMConfig<T> fSMConfig)
    {
        this.AbstractFSMConfig = fSMConfig;
    }

    public abstract FSMState TransitionateState(T fromState, T toState);
}
