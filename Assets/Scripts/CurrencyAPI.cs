using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

public class CurrencyAPI
{
    static string uriBase = "https://api.exchangeratesapi.io/";
    static HttpClient client = new HttpClient();

    private static string SendGetRequest(string uri)
    {
        var responseTask = client.GetStringAsync(uriBase + uri).Result;
        return responseTask;
    }

    public static CurrencyData GetCurrencyData(DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");
        // Debug.Log("submitted API request. Date: " + formattedDate);
        var response = SendGetRequest(formattedDate);
        
        var rates = new CurrencyData();
        if (response != null)
        {
            JObject parsedResponse = JObject.Parse(response);
            // NOTE: expected response structure:
            // rates, a dictionary of currency/rate pairs
            // base, should be EUR
            // date, new data set is provided every work day

            IDictionary<string, JToken> ratesObj = (JObject) parsedResponse["rates"];
            rates.rates = ratesObj.ToDictionary(pair => pair.Key, pair => (decimal) pair.Value);

            rates.lastUpdate = DateTime.Parse(parsedResponse["date"].ToString());

            // Debug.Log("Currency dataset successfully added for " + date);
        }
        return rates;
    }
}
