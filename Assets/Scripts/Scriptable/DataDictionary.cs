using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class DataDictionary : ScriptableObject
{

    [Expandable] // TODO: make this attribute work with dictionaries
    public Dictionary<DateTime, CurrencyData> data;
    private string dataPath = "currencyData.asset";
    public int Count
    {
        get => data.Count;
    }

    public void Add(DateTime date, CurrencyData entry)
    {
        if (data == null) data = new Dictionary<DateTime, CurrencyData>();
        data.Add(date, entry);
        Save();
    }

    public bool HasEntry(DateTime key)
    {
        var result = false;
        if (data?.Count > 0)
        {
            result = data.ContainsKey(key);
        }

        return result;
    }

    public CurrencyData GetEntry(DateTime date)
    {
        if (!HasEntry(date))
        {
            Add(date, CurrencyAPI.GetCurrencyData(date));
        }
        CurrencyData result = null;
        data.TryGetValue(date, out result);
        return result;
    }

    public void Clear()
    {
        data.Clear();
        Save();
    }

    public void Load()
    {
        data = FileSystem.ReadBin<Dictionary<DateTime, CurrencyData>>(dataPath);
    }

    private void Save()
    {
        FileSystem.CreateWriteBin<Dictionary<DateTime, CurrencyData>>(dataPath, data);
    }

}
