using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleFSM<T, W> : GenericFSM<T, W> where T : Enum where W : AbstractFSMData
{
    public SimpleFSM()
    {
    }

    public SimpleFSM(Dictionary<T, FSMState<W>> stateConfiguration)
    {
        FSMConfig<T, W> simpleConfig = new FSMConfig<T, W>(this.GetAllValuesFromT(), stateConfiguration);
        FSMForcedTransitioner<T, W> forcedTransitioner = new FSMForcedTransitioner<T, W>(simpleConfig);

        this.FSMConfig = simpleConfig;
        this.FSMTransitioner = forcedTransitioner;
    }

    private FSMStateLinksData<T> GetAllValuesFromT()
    {
        FSMStateLinksData<T> data = new FSMStateLinksData<T>();
        IEnumerable<T> TValues = Enum.GetValues(typeof(T)).Cast<T>();

        foreach (var type in TValues)
            foreach (var otherType in TValues)
                data.Add(FSMConfig<T, W>.StateToConfig(type, otherType));

        return data;
    }
}