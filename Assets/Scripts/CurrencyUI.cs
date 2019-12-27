using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Dropdown types;
    public InputField value;

    public void SetValue(CurrencyData currentRates, string cur0pref)
    {
        if (currentRates.HasEntry(cur0pref))
        {
            for (int i = 0; i < types.options.Count; i++)
            {
                var option = types.options[i];
                if (option.text == cur0pref)
                {
                    types.value = i;
                }
            }
            value.text = cur0pref;
        }
        else if (currentRates.baseCurrency == cur0pref)
        {
            types.value = 0;
        }
        else
        {
            Debug.LogError(cur0pref + "currency not found");
        }
    }
}
