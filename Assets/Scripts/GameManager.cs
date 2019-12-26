using System;
using pingak9;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text debugLogger;
    public void DebugLog(string log)
    {
        debugLogger.text = log;
        Debug.Log(log);
    }

    public void OnDatePicker()
    {
        var today = DateTime.Today;
        DebugLog("Started date picking");
        NativeDialog.OpenDatePicker(today.Year, today.Month, today.Day,
            (DateTime _date) =>
            {
                DebugLog(_date.ToString());
            },
            (DateTime _date) =>
            {
                DebugLog(_date.ToString());
            });
    }
}
