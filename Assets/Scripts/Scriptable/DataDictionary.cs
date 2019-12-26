using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class DataDictionary : ScriptableObject
{

    [Expandable] // TODO: make this attribute work with dictionaries
    public Dictionary<DateTime, CurrencyData> data = new Dictionary<DateTime, CurrencyData>();

    public int Count
    {
        get => data.Count;
    }

    public void Add(DateTime date, CurrencyData entry)
    {
        data.Add(date, entry);
        SaveData();
    }

    public bool HasEntry(DateTime key)
    {
        var result = false;
        if (data.Count > 0)
        {
            result = data.ContainsKey(key);
        }

        return result;
    }

    public void Clear()
    {
        data.Clear();
        SaveData();
    }

    public void Load()
    {
        var dataPath = Application.persistentDataPath + "/currencyData.cache";
        if (File.Exists(dataPath))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.OpenRead(Application.persistentDataPath + "/currencyData.cache");
            if (file.Length > 0)
            {
                data = (Dictionary<DateTime, CurrencyData>) binaryFormatter.Deserialize(file);
                file.Close();
            }
        }
    }

    private void SaveData()
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/currencyData.cache");
        binaryFormatter.Serialize(file, data);
        file.Close();
    }
}
