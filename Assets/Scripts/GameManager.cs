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
        var dateParsed = date.ToString("dd MMM yyyy");
        dateOutput.text = dateParsed;
    }

    public void OnDatePicker()
    {
        DebugLog("Started date picking");
        NativeDialog.OpenDatePicker(date.Year, date.Month, date.Day,
            (DateTime _date) =>
            {
                date = _date;
                DebugLog(_date.ToString());
            },
            (DateTime _date) =>
            {
                date = _date;
                DebugLog(_date.ToString());
            });
    }
}
