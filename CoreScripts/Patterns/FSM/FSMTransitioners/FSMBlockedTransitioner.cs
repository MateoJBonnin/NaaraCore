﻿using System;

public class FSMBlockedTransitioner<T, W> : AbstractFSMTransitioner<T, W> where T : Enum where W : AbstractFSMData
{
    public override FSMState<W> TransitionateState(T fromState, T toState)
    {
        return null;
    }
}