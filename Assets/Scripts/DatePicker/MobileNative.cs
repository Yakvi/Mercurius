// Modified snippet from Mobile Dialog Unity by PingAK9, MIT license. https://unitylist.com/p/dp9/Mobile-Dialog-Unity 

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace pingak9
{

    public class MobileNative
    {

#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern void _TAG_ShowDatePicker(int mode, double unix);

#endif

        public static void showDatePicker(int year, int month, int day)
        {
#if UNITY_EDITOR
#elif UNITY_IPHONE
            DateTime dateTime = new DateTime(year, month, day);
            double unix = (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
            _TAG_ShowDatePicker(2, unix);
#elif UNITY_ANDROID
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.pingak9.nativepopup.Bridge");
            javaUnityClass.CallStatic("ShowDatePicker", year, month, day);
#endif
        }
    }
}
