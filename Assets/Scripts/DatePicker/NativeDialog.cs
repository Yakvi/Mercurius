using UnityEngine;
using System.Collections;
using System;

namespace pingak9
{
    public class NativeDialog
    {
        public NativeDialog() { }

        public static void OpenDatePicker(int year , int month, int day, Action<DateTime> onChange = null, Action<DateTime> onClose = null)
        {
            MobileDateTimePicker.CreateDate(year, month, day, onChange, onClose);
        }
        public static void OpenTimePicker(Action<DateTime> onChange = null, Action<DateTime> onClose = null)
        {
            MobileDateTimePicker.CreateTime(onChange, onClose);
        }
    }
}