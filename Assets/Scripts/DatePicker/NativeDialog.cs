// Modified snippet from Mobile Dialog Unity by PingAK9, MIT license. https://unitylist.com/p/dp9/Mobile-Dialog-Unity 

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
    }
}