using System;
using pingak9;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    DateTime date = DateTime.Today;
    [SerializeField]
    Text dateOutput;
    [SerializeField]
    CurrencyUI[] currencies;
    [SerializeField]
    DataDictionary dataCache;

    // DEBUG: clear on release
    [SerializeField]
    Text debugLogger;
    public void DebugLog(string log)
    {
        debugLogger.text = log;
        Debug.Log(log);
    }

    void Start()
    {
        dataCache.Load();
        if (!dataCache.HasEntry(date))
        {
            CurrencyAPI.GetCurrencyData(dataCache, date);
            DebugLog("Started adding today's data");
        }
    }

    void Update()
    {

        dateOutput.text = date.ToString("dd MMM yyyy");
    }

    public void OnDatePicker()
    {
        DebugLog("Started date picking");
        NativeDialog.OpenDatePicker(date.Year, date.Month, date.Day, SetDate, SetDate);
    }

    private void SetDate(DateTime _date)
    {
        date = _date;
        DebugLog(_date.ToString());
    }
}
