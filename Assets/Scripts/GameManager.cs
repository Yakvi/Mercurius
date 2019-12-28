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
    public ScrollRect currencyList;
    public GameObject currencyTemplate;

    public DataDictionary dataCache;
    public CurrencyData currentRates;

    private DateTime date;
    private CurrencyUI currencySelector;

    void Start()
    {
        dataCache.Load(); // In case we have a saved database on disk
        SetDate(DateTime.Today); // Default the database to today
        FieldsInit(); // Populate fields
    }

    #region Callback Functions

    public void OnCurrencySelect(CurrencyUI source)
    {
        currencyList.gameObject.SetActive(true);
        currencySelector = source;
    }

    public void OnCurrencyChanged(string value)
    {
        currencyList.gameObject.SetActive(false);
        var source = currencySelector;
        var target = FindTarget(source);

        source.type = value;
        UpdatePlayerPrefs(source, target);
        RecalculateRate(target, source); // We change currency, so here target and source flip places
    }

    public void OnFieldsSwap()
    {
        var temp = (type: currencies[1].type, amount: currencies[1].amount);
        currencies[1].type = currencies[0].type;
        currencies[1].amount = currencies[0].amount;

        currencies[0].type = temp.type;
        currencies[0].amount = temp.amount;
    }

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
        PlayerPrefs.SetString(source.tag, source.type);
        PlayerPrefs.SetString(source.tag, source.type);
    }

    /// <summary>
    /// source's values must remain the same, targets values must change.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void RecalculateRate(CurrencyUI source, CurrencyUI target)
    {
        decimal sourceRate = currentRates.getRate(source.type);
        decimal targetRate = currentRates.getRate(target.type);

        if (sourceRate != 0)
        {
            var baseCurrency = source.amount / sourceRate;
            target.amount = baseCurrency * targetRate;
        }
    }

    /// <summary>
    /// Starting function to clean up the UI and populate the dropdowns
    /// </summary>
    private void FieldsInit()
    {
        var currencyJson = Resources.Load<TextAsset>("currencies");
        var currencyNames = JsonUtility.FromJson<CurrencyJson>(currencyJson.text);
        foreach (var currency in currencyNames.currencies)
        {
            if (currentRates.HasEntry(currency.code))
            {
                var button = Instantiate(currencyTemplate, currencyList.content);
                button.GetComponent<CurrencyButton>().code.text = currency.code;
                button.GetComponent<CurrencyButton>().designation.text = currency.name;
                button.GetComponent<Button>().onClick.AddListener(() => OnCurrencyChanged(currency.code));
            }
        }
        currencyList.gameObject.SetActive(false);

        currencies[0].SetDefaultCurrency(currentRates, "EUR");
        currencies[1].SetDefaultCurrency(currentRates, "USD");

        currencies[0].valueInput.text = "1.00";
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

[System.Serializable]
public class CurrencyJson
{
    public Currency[] currencies;
}

[System.Serializable]
public class Currency
{
    public string name;
    public string code;
}
