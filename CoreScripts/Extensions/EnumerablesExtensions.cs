using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class EnumerablesExtensions
{
    public static bool In<T>(this T x, HashSet<T> set)
    {
        return set.Contains(x);
    }

    public static bool IsEmpty<T>(this List<T> x)
    {
        return x.Count <= 0;
    }

    public static bool IsEmpty<T>(this Queue<T> x)
    {
        return x.Count <= 0;
    }

    public static void RemoveFirst<T>(this List<T> x)
    {
        if (x.Count > 0)
            x.RemoveAt(0);
    }

    public static bool In<K, V>(this KeyValuePair<K, V> x, Dictionary<K, V> dict)
    {
        return dict.Contains(x);
    }

    public static void ForEach<K, V>(this Dictionary<K, V> dict, Action<KeyValuePair<K, V>> action)
    {
        foreach (KeyValuePair<K, V> kv in dict)
            action(kv);
    }

    public static V DefaultGet<K, V>(this Dictionary<K, V> dict, K key, Func<V> defaultFactory)
    {
        V v;
        if (!dict.TryGetValue(key, out v))
            dict[key] = v = defaultFactory();
        return v;
    }

    public static void Update<K, V>(this Dictionary<K, V> a, Dictionary<K, V> b)
    {
        foreach (var kvp in b)
        {
            a[kvp.Key] = kvp.Value;
        }
    }
}
