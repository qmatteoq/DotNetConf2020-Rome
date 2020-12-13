using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WinUISample.Exceptions;

namespace WinUISample.Services
{
    public class BaseProvider
    {
        protected string _baseUrl;

        protected HttpClient GetClient()
        {
            return GetClient(_baseUrl);
        }

        protected virtual HttpClient GetClient(string baseUrl)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            return client;
        }

        protected async Task Get(string url)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadFromJsonAsync<TrackSeriesApiError>();
                        throw new TrackSeriesApiException(error.Message, response.StatusCode);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new TrackSeriesApiException("", false, ex);
                }
            }
        }

        protected async Task<T> Get<T>(string url)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadFromJsonAsync<TrackSeriesApiError>();
                        var message = error != null ? error.Message : "";
                        throw new TrackSeriesApiException(message, response.StatusCode);
                    }
                    string result = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(result);
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                catch (HttpRequestException ex)
                {
                    throw new TrackSeriesApiException("", false, ex);
                }
            }
        }
    }
}
