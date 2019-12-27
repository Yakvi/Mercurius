using System;
using System.Collections.Generic;
using pingak9;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string version = "0.0.5";
    private string build = "11";

    DateTime date;
    public Text dateOutput;
    
    public CurrencyUI[] currencies;
    public DataDictionary dataCache;
    public CurrencyData currentRates;

    // DEBUG: clear on release
    public Text debugLogger, debugVersion;
    public void DebugLog(string log)
    {
        debugLogger.text = log;
        Debug.Log(log);
    }

    void Start()
    {
        debugVersion.text = "ver." + version + " build " + build;

        dataCache.Load();

        SetDate(DateTime.Today);
    }

    public void DEBUGOnFileRead()
    {
        var result = FileSystem.ReadBin<Dictionary<DateTime, CurrencyData>>("currencyData.cache");
        DebugLog(result.ToString());
    }

    public void OnDatePicker()
    {
        DebugLog("Started date picking");
        NativeDialog.OpenDatePicker(date.Year, date.Month, date.Day, SetDate, SetDate);
    }

    private void SetDate(DateTime _date)
    {
        date = _date;

        DebugLog("Submitted data cache request. Data Cache size: " + dataCache.data.Keys.Count);
        // currentRates = dataCache.GetEntry(date);
        // dateOutput.text = date.ToString("dd MMM yyyy");
        // foreach (var outputUI in currencies)
        // {
        //     outputUI.types.ClearOptions();
        //     outputUI.types.AddOptions(new List<string>(currentRates.rates.Keys));
        // }
        // string result = "Date selection completed. \n";
        // result+= "data updated on:\n" + currentRates.lastUpdate.ToString();
        // DebugLog(result);

    }
}
