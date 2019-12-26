using System;
using pingak9;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    DateTime date = DateTime.Today;
    [SerializeField]
    Text debugLogger, dateOutput;
    public void DebugLog(string log)
    {
        debugLogger.text = log;
        Debug.Log(log);
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
