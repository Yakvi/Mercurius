using System;
using System.Collections.Generic;
using pingak9;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI dateOutput;
    public CurrencyUI[] currencies;

    public DataDictionary dataCache;
    public CurrencyData currentRates;

    private DateTime date;

    void Start()
    {
        dataCache.Load(); // In case we have a saved database on disk
        SetDate(DateTime.Today); // Default the database to today
        FieldsInit(); // Populate fields
    }

    #region Callback Functions

    public void OnDatePicker()
    {
        // DebugLog("Started date picking");
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
        UpdatePlayerPrefs(source, target);
        RecalculateRate(target, source); // We change currency, so here target and source flip places
    }

    /// <summary>
    /// Main Callback function used during date picking. GetEntry also calls API in case the cache doesn't have entry for that date.
    /// </summary>
    /// <param name="_date"></param>
    public void SetDate(DateTime _date)
    {
        // var result = "Submitted data cache request. Date received: " + _date.ToString("yyyy-MM-dd") + "\n";
        date = _date;
        currentRates = dataCache.GetEntry(date);

        dateOutput.text = "For the selected date, (";
        dateOutput.text += _date.ToString("dd MMM yyyy");
        dateOutput.text += "),\nConversion rates were updated on\n";
        dateOutput.text += currentRates.lastUpdate.ToString("dd MMM yyyy");

        RecalculateRate(currencies[0], currencies[1]);

        // result += "Data loaded: " + currentRates.lastUpdate.ToString("yyyy-MM-dd") + "\n";
        // result += "Rates loading completed. \n";
        // DebugLog(result);

    }
    #endregion

    #region Private Methods

    /// <summary>
    /// Select a random target. 
    /// If it has the same tag as the source, select the other.
    /// Tags must be set and different. UI elements are expected to be in pairs of two
    /// </summary>
    /// <param name="source">UI elements which contain the original currency</param>
    /// <returns>Returns the other element of the two.</returns>
    private CurrencyUI FindTarget(CurrencyUI source)
    {
        var target = currencies[1];
        if (source.CompareTag(target.tag)) target = currencies[0];
        return target;
    }

    /// <summary>
    /// Save the preferences for future use (so that on reload user has the same items).
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void UpdatePlayerPrefs(CurrencyUI source, CurrencyUI target)
    {
        PlayerPrefs.SetString(source.tag, source.types.options[source.types.value].text);
        PlayerPrefs.SetString(source.tag, source.types.options[source.types.value].text);
    }

    /// <summary>
    /// source's values must remain the same, targets values must change.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void RecalculateRate(CurrencyUI source, CurrencyUI target)
    {
        if (source.value.text != "")
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

            var sourceAmount = decimal.Parse(source.value.text) / sourceRate;
            var targetAmount = sourceAmount * targetRate;
            target.value.SetTextWithoutNotify(Math.Round(targetAmount, 2).ToString());
        }
    }

    /// <summary>
    /// Starting function to clean up the UI and populate the dropdowns
    /// </summary>
    private void FieldsInit()
    {
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

        var pref = PlayerPrefs.GetString(currencies[0].tag, "EUR");
        currencies[0].SetCurrency(currentRates, pref);
        pref = PlayerPrefs.GetString(currencies[1].tag, "USD");
        currencies[1].SetCurrency(currentRates, pref);

        currencies[0].value.text = "1.00";
    }
    #endregion

    #region Debug Fields and Calls
    public Text debugLogger, debugVersion;
    // private string DEBUGversion = "1.0.0";
    // private string DEBUGbuild = "17";
    public void DebugLog(string log)
    {
        // debugLogger.text = log;
        // Debug.Log(log);
    }
    public void DEBUGOnFileRead()
    {
        // var result = FileSystem.ReadBin<Dictionary<DateTime, CurrencyData>>("currencyData.asset");
        // DebugLog(result.ToString());
    }
    public void DEBUGSetDate(InputField field)
    {
        // SetDate(DateTime.Parse(field.text));
    }
    #endregion
}
