using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubManager
{
    public SimpleFSM<SubManagerReadyStates, EmptyFSMStateData> subManagerStateFSM;
    public Action OnSubManagerReady { get; set; }

    public SubManager()
    {
        this.subManagerStateFSM = new SimpleFSM<SubManagerReadyStates, EmptyFSMStateData>(this.GetSubManagerReadyConfig());
        this.OnSubManagerReady += () => this.subManagerStateFSM.Feed(SubManagerReadyStates.Ready);
    }

    public virtual void UpdateSubManager()
    {
    }

    private Dictionary<SubManagerReadyStates, FSMState<EmptyFSMStateData>> GetSubManagerReadyConfig()
    {
        Dictionary<SubManagerReadyStates, FSMState<EmptyFSMStateData>> connections = new Dictionary<SubManagerReadyStates, FSMState<EmptyFSMStateData>>();

        connections[SubManagerReadyStates.NotReady] = new SimpleFSMState();
        connections[SubManagerReadyStates.Ready] = new SimpleFSMState();

        return connections;
    }
}


public enum SubManagerReadyStates
{
    NotReady,
    Ready,
}