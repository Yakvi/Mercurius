using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currency;
    public TMP_InputField valueInput;

    public decimal amount
    {
        get { return (valueInput.text == "") ? 0 : decimal.Parse(valueInput.text); }
        set { valueInput.SetTextWithoutNotify(Math.Round(value, 4).ToString()); }
    }

    public string type
    {
        get { return currency.text; }
        set { currency.text = value; }
    }

    public void SetDefaultCurrency(CurrencyData data, string defaultValue)
    {
        var pref = PlayerPrefs.GetString(gameObject.tag, defaultValue);
        if (data.HasEntry(pref)) type = pref;
    }
}
