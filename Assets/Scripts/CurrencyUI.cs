using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Dropdown types;
    public InputField valueInput;

    public decimal amount
    {
        get { return (valueInput.text == "") ? 0 : decimal.Parse(valueInput.text); }
        set { valueInput.SetTextWithoutNotify(Math.Round(value, 2).ToString()); }
    }

    public string type
    {
        get { return types.options[types.value].text; }
        set
        {
            for (int i = 0; i < types.options.Count; i++)
            {
                var option = types.options[i].text;
                if (option == value)
                {
                    types.SetValueWithoutNotify(i);
                    break;
                }
            }
        }
    }

    public void SetDefaultCurrency(CurrencyData data, string defaultValue)
    {
        var pref = PlayerPrefs.GetString(gameObject.tag, defaultValue);
        if (data.HasEntry(pref)) type = pref;
    }
}
