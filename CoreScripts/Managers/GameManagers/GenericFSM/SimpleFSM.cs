using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleFSM<T> : GenericFSM<T> where T : Enum
{
    public SimpleFSM()
    {
    }

    public SimpleFSM(Dictionary<T, FSMState> stateConfiguration)
    {
        FSMConfig<T> simpleConfig = new FSMConfig<T>(this.GetAllValuesFromT(), stateConfiguration);
        FSMForcedTransitioner<T> forcedTransitioner = new FSMForcedTransitioner<T>(simpleConfig);

        this.FSMConfig = simpleConfig;
        this.FSMTransitioner = forcedTransitioner;
    }

    private JSONObject GetAllValuesFromT()
    {
        JSONObject data = new JSONObject();
        IEnumerable<T> TValues = Enum.GetValues(typeof(T)).Cast<T>();

        foreach (var type in TValues)
            foreach (var otherType in TValues)
                data.Add(FSMConfig<T>.StateToConfig(type, otherType));

        return data;
    }
}