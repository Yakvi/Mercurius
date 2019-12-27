using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

public class CurrencyAPI
{
    static string uriBase = "https://api.exchangeratesapi.io/";
    static HttpClient client = new HttpClient();

    private static async Task<string> SendGetRequest(string uri)
    {
        var responseTask = client.GetStringAsync(uriBase + uri);
        return await responseTask;
    }

    public static async void GetCurrencyData(DataDictionary db, DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");
        Debug.Log("submitted API request. Date: " + formattedDate);
        var responseTask = SendGetRequest(formattedDate);
        var response = await responseTask;

        if (response != null)
        {
            var rates = new CurrencyData();
            JObject parsedResponse = JObject.Parse(response);
            // NOTE: expected response structure:
            // rates, a dictionary of currency/rate pairs
            // base, should be EUR
            // date, new data set is provided every work day

            IDictionary<string, JToken> ratesObj = (JObject) parsedResponse["rates"];
            rates.rates = ratesObj.ToDictionary(pair => pair.Key, pair => (decimal) pair.Value);

            rates.lastUpdate = DateTime.Parse(parsedResponse["date"].ToString());

            db.Add(date, rates);
            Debug.Log("Currency dataset successfully added for " + date);
        }
    }
}
