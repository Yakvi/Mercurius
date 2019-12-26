using UnityEngine;
using System.Collections;
using pingak9;
using UnityEngine.UI;
using System;
using System.Globalization;

public class PopupView : MonoBehaviour
{
    public Text txtLog;
    public void DebugLog(string log)
    {
        txtLog.text = log;
        Debug.Log(log);
    }

    public void OnDatePicker()
    {
        var today = DateTime.Today;
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
    public void OnTimePicker()
    {
        NativeDialog.OpenTimePicker(
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
