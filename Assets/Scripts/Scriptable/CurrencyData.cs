using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CurrencyData
{
    public string baseCurrency = "EUR";
    public DateTime lastUpdate;
    public Dictionary<string, decimal> rates;

    public bool HasEntry(string key)
    {
        var result = false;
        if (rates?.Count > 0)
        {
            result = rates.ContainsKey(key);
        }

        return result;
    }
}
