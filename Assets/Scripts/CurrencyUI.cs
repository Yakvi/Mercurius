using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Dropdown types;
    public InputField value;

    public void SetCurrency(CurrencyData currentRates, string pref)
    {
        var currencyIndex = 0;
        if (currentRates.HasEntry(pref))
        {
            for (int i = 0; i < types.options.Count; i++)
            {
                var option = types.options[i];
                if (option.text == pref)
                {
                    currencyIndex = i;
                }
            }
            value.text = pref;
        }
        else if (currentRates.baseCurrency == pref)
        {
            currencyIndex = 0;
        }
        else
        {
            // Debug.LogError(pref + "currency not found");
        }
        types.SetValueWithoutNotify(currencyIndex);
    }
}
