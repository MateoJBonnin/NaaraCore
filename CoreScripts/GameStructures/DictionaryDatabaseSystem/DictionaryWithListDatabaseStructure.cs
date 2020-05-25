using System;
using System.Collections.Generic;

public class GenericDatabase<T, W>
{
    private Dictionary<T, List<W>> database;

    public GenericDatabase()
    {
        this.database = new Dictionary<T, List<W>>();
    }

    public GenericDatabase(Dictionary<T, List<W>> database)
    {
        this.database = database;
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

    public T GetKeyByData(W t)
    {
        foreach (KeyValuePair<T, List<W>> dataKV in database)
        {
            dataKV.Value.Contains(t);
            return dataKV.Key;
        }

        return default(T);
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

    public void RemoveKeyFrom(T t)
    {
        if (!this.database.ContainsKey(t))
            return;

        this.database.Remove(t);
    }

    public void ClearAllDataFrom(T t)
    {
        if (!this.database.ContainsKey(t))
            return;

        this.database[t] = new List<W>();
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
        return this.database.DefaultGet(t, () => new List<W>());
    }

    public Dictionary<T, List<W>> GetAllData()
    {
        return this.database;
    }

    public bool ContainsAnyData()
    {
        return this.database.Count > 0;
    }

    public int GetKeysCount()
    {
        return this.database.Keys.Count;
    }

    public int GetValueCount(T key)
    {
        return this.database.ContainsKey(key) ? this.database[key].Count : 0;
    }

    public void ClearData()
    {
        this.database.Clear();
    }
}