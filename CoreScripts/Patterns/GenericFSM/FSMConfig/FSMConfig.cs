using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMConfig<T> where T : Enum
{
    public const string STATE_FROM_KEY = "stateFrom";
    public const string STATE_TO_KEY = "stateTo";

    public static Dictionary<string, T> TypeNamesToT
    {
        get
        {
            if (typeNamesToT == null)
                ConvertTypeToStringName();
            return typeNamesToT;
        }
        private set
        {
            typeNamesToT = value;
        }
    }

    private static Dictionary<string, T> typeNamesToT;

    public Dictionary<T, List<T>> configTransitions
    { get; private set; }
    public Dictionary<T, FSMState> statesDatabase { get; private set; }

    public FSMConfig()
    {
        this.Init();
    }

    public FSMConfig(JSONObject configData, Dictionary<T, FSMState> statesData)
    {
        this.Init();
        this.ConfigureStates(statesData);
        this.ConfigureConnections(configData);
    }

    public virtual void ConfigureConnections(JSONObject configData)
    {
        foreach (JSONObject stateConn in configData.list)
            this.SetTransition(stringNameToT(stateConn[STATE_FROM_KEY].str), stringNameToT(stateConn[STATE_TO_KEY].str));
    }

    public virtual void ConfigureStates(Dictionary<T, FSMState> statesData)
    {
        foreach (KeyValuePair<T, FSMState> state in statesData)
            this.SetState(state.Key, state.Value);
    }

    public void Init()
    {
        this.configTransitions = new Dictionary<T, List<T>>();
        this.statesDatabase = new Dictionary<T, FSMState>();
    }

    public FSMState GetStateFromTransition(T from, T to)
    {
        List<T> possibleStates;
        this.configTransitions.TryGetValue(from, out possibleStates);

        if (null != possibleStates && possibleStates.Contains(to))
            return this.statesDatabase[to];

        return null;
    }

    public FSMState GetState(T stateType)
    {
        FSMState state;
        this.statesDatabase.TryGetValue(stateType, out state);
        return state;
    }

    public T GetTypeByState(FSMState stateType)
    {
        foreach (var state in this.statesDatabase)
        {
            if (state.Value == stateType)
                return state.Key;
        }

        return default(T);
    }

    public void SetState(T stateKey, FSMState fSMState)
    {
        this.statesDatabase[stateKey] = fSMState;
    }

    public void RemoveState(T stateKey, FSMState fSMState)
    {
        this.statesDatabase.Remove(stateKey);
    }

    public void SetTransition(T from, T to)
    {
        List<T> transitions = null;
        this.configTransitions.TryGetValue(from, out transitions);
        if (null == transitions)
            this.configTransitions[from] = new List<T>() { to };
        else
        {
            transitions.Add(to);
            this.configTransitions[from] = transitions;
        }
    }

    public void RemoveTransition(T from, T to)
    {
        List<T> transitions = null;
        this.configTransitions.TryGetValue(from, out transitions);
        if (null != transitions)
        {
            transitions.Remove(to);
            this.configTransitions[from] = transitions;
        }
    }

    public static JSONObject StateToConfig(T from, T to)
    {
        JSONObject config = new JSONObject();
        config.AddField(STATE_FROM_KEY, from.ToString());
        config.AddField(STATE_TO_KEY, to.ToString());
        return config;
    }

    public static T stringNameToT(string name)
    {
        return TypeNamesToT[name];
    }

    private static void ConvertTypeToStringName()
    {
        typeNamesToT = new Dictionary<string, T>();
        foreach (object type in Enum.GetValues(typeof(T)))
            typeNamesToT[type.ToString()] = (T)type;
    }
}
