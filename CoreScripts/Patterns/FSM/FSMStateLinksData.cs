using System;
using System.Collections.Generic;

public class FSMStateLinksData<T>
{
    public List<FSMStateLink<T>> linksData;

    public FSMStateLinksData()
    {
        this.linksData = new List<FSMStateLink<T>>();
    }

    public FSMStateLinksData(List<FSMStateLink<T>> linksData)
    {
        this.linksData = linksData;
    }

    public void Add(FSMStateLink<T> toAdd)
    {
        this.linksData.Add(toAdd);
    }

    public void Remove(FSMStateLink<T> toRemove)
    {
        this.linksData.Add(toRemove);
    }
}
