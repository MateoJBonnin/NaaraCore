using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryWithListDatabaseStructure<T, W>
{
    private Dictionary<T, List<W>> database;

    public DictionaryWithListDatabaseStructure()
    {
        this.database = new Dictionary<T, List<W>>();
    }

    public void RegisterData(T t, W w)
    {
        List<W> tempWList = new List<W>();
        List<W> databaseWList = new List<W>();

        if (this.database.ContainsKey(t))
            databaseWList = this.database[t];

        databaseWList.Add(w);
        tempWList.AddRange(databaseWList);
        this.database[t] = tempWList;
    }

    public void RemoveData(T t, W w)
    {
        if (!this.database.ContainsKey(t))
            return;

        for (int i = this.database[t].Count - 1; i >= 0; i--)
        {
            W data = this.database[t][i];
            if (w.Equals(data))
                this.database[t].Remove(this.database[t][i]);
        }
    }

    public W SearchDataByPredicate(T t, Func<W, bool> predicate)
    {
        List<W> data = this.GetData(t);
        for (int i = data.Count - 1; i >= 0; i--)
        {
            if (predicate(data[i]))
                return data[i];
        }

        return default(W);
    }

    public void SearchAndRemoveDataByPredicate(T t, Func<W, bool> predicate)
    {
        this.RemoveData(t, this.SearchDataByPredicate(t, predicate));
    }

    public List<W> GetData(T t)
    {
        List<W> data;
        this.database.TryGetValue(t, out data);

        return data;
    }

    public void ClearData()
    {
        this.database.Clear();
    }
}