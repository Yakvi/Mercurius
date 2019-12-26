using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CurrencyData
{
    public string baseCurrency = "EUR";
    public DateTime lastUpdate;
    public Dictionary<string, decimal> rates;
}
