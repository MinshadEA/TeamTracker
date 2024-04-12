using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TeamTracker.Helper
{
    public class HttpHelper
    {
        Uri baseAddress = new Uri(URL.BaseURl);
        string apiurl;
        string postdata;
        string stringObtained;
        string receivedMethod;
        private HttpRequestMessage req;
        public HttpHelper(string apiurl, string postdata, string method)
        {
            this.apiurl = apiurl;
            this.postdata = postdata;
            this.receivedMethod = method;
        }
        public T APICallResult<T>()
        {

            try
            {
                var jsonObtained = "";
                string stringObtained = "";
                var client = new HttpClient { BaseAddress = baseAddress };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    if (receivedMethod == APIMethodConstants.GET)
                    {
                        req = new HttpRequestMessage(HttpMethod.Get, apiurl);
                        Task<string> task = Task.Run(async () => await Threading(client, req));
                        task.Wait();
                        stringObtained = task.Result;
                        jsonObtained = stringObtained;
                    }
                    else if (receivedMethod == APIMethodConstants.POST)
                    {

                        req = new HttpRequestMessage(HttpMethod.Post, apiurl);

                        req.Content = new StringContent(postdata, Encoding.UTF8, "application/json");
                        Task<string> task = Task.Run(async () => await Threading(client, req));
                        task.Wait();
                        stringObtained = task.Result;
                        jsonObtained = Regex.Unescape(stringObtained);
                    }
                }
                var resultJSON = jsonObtained;
                T resultObject;//Generic type object
                try
                {
                    resultObject = JsonConvert.DeserializeObject<T>(resultJSON);
                    return resultObject;
                }
                catch (Exception e)
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
        async Task<string> Threading(HttpClient client, HttpRequestMessage req)
        {
            var resp = await client.SendAsync(req);
            switch (resp.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    stringObtained = await resp.Content.ReadAsStringAsync();
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    stringObtained = await resp.Content.ReadAsStringAsync();
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    stringObtained = await resp.Content.ReadAsStringAsync();
                    break;
                default:
                    stringObtained = await resp.Content.ReadAsStringAsync();
                    break;
            }
            return stringObtained;
        }
    }
}

