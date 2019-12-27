using System;
using System.Collections.Generic;
using pingak9;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string version = "0.0.6";
    private string build = "13";

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
    public void DEBUGOnFileRead()
    {
        var result = FileSystem.ReadBin<Dictionary<DateTime, CurrencyData>>("currencyData.cache");
        DebugLog(result.ToString());
    }

    void Start()
    {
        debugVersion.text = "ver." + version + " build " + build;

        dataCache.Load();

        SetDate(DateTime.Today);

        FieldsInit();
    }

    public void OnDatePicker()
    {
        DebugLog("Started date picking");
        NativeDialog.OpenDatePicker(date.Year, date.Month, date.Day, SetDate, SetDate);
    }

    public void OnValueChanged(CurrencyUI source)
    {
        var target = FindTarget(source);
        RecalculateRate(source, target);
    }

    public void OnCurrencyChanged(CurrencyUI source)
    {
        var target = FindTarget(source);
        UpdatePrefs(source, target);
        RecalculateRate(source, target);
    }

    /// <summary>
    /// Select a random target. 
    /// If it has the same tag as the source, select the other.
    /// Tags must be set and different.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private CurrencyUI FindTarget(CurrencyUI source)
    {
        var target = currencies[1];
        if (source.CompareTag(target.tag)) target = currencies[0];
        return target;
    }

    private void UpdatePrefs(CurrencyUI source, CurrencyUI target)
    {
        PlayerPrefs.SetString(currencies[0].tag, currencies[0].types.itemText.text);
        PlayerPrefs.SetString(currencies[1].tag, currencies[1].types.itemText.text);
    }

    /// <summary>
    /// source's values must remain the same, targets values must change.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void RecalculateRate(CurrencyUI source, CurrencyUI target)
    {
        if (source.value.text != "" && target.value.text != "")
        {
            var sourceType = source.types.options[source.types.value].text;
            var targetType = target.types.options[target.types.value].text;
            // Check if rebase is needed
            decimal sourceRate = 1;
            if (sourceType != currentRates.baseCurrency)
            {
                currentRates.rates.TryGetValue(sourceType, out sourceRate);
            }
            decimal targetRate = 1;
            if (targetType != currentRates.baseCurrency)
            {
                currentRates.rates.TryGetValue(targetType, out targetRate);
            }

            decimal sourceAmount = decimal.Parse(source.value.text) * sourceRate;
            target.value.SetTextWithoutNotify((sourceAmount * targetRate).ToString());
        }
    }

    private void SetDate(DateTime _date)
    {
        date = _date;

        currentRates = dataCache.GetEntry(date);
        DebugLog("Submitted data cache request. Data Cache size: " + dataCache.data.Keys.Count);
        dateOutput.text = date.ToString("dd MMM yyyy");

        foreach (var outputUI in currencies)
        {
            outputUI.types.ClearOptions();
            if (currentRates?.rates?.Keys.Count > 0)
            {
                var ratesList = new List<string>(currentRates.rates.Keys);
                ratesList.Insert(0, currentRates.baseCurrency);
                outputUI.types.AddOptions(ratesList);
            }
        }

        string result = "Date selection completed. \n";
        result += "data updated on:\n" + currentRates.lastUpdate.ToString();
        DebugLog(result);

    }

    private void FieldsInit()
    {
        var pref = PlayerPrefs.GetString(currencies[0].tag, "EUR");
        currencies[0].SetValue(currentRates, pref);
        pref = PlayerPrefs.GetString(currencies[1].tag, "USD");
        currencies[1].SetValue(currentRates, pref);

        currencies[0].value.text = "1";
    }
}
