using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SONetworking
{
    public static class SOJsonRequestOperation
    {
        public static HttpClient client = new HttpClient();
        public static string BaseUrl;

        public static void StartJSONRequestOperation(HttpMethod method, string path, List<KeyValuePair<string, string>> parameters, Action<bool, int, JObject> callback, List<KeyValuePair<string, string>> headerParams = null)
        {
            if (method == HttpMethod.Get)
            {
                StartJSONRequestOperationGet(path, parameters, callback, headerParams);
            }
            if (method == HttpMethod.Post)
            {
                StartJSONRequestOperationPost(path, parameters, callback, headerParams);
            }
            if (method == HttpMethod.Put)
            {
                StartJSONRequestOperationPut(path, parameters, callback, headerParams);
            }
            if (method == HttpMethod.Delete)
            {
                StartJSONRequestOperationDelete(path, parameters, callback, headerParams);
            }           
        }

        public static async void StartJSONRequestOperationPost(string path, List<KeyValuePair<string, string>> parameters, Action<bool, int, JObject> callback, List<KeyValuePair<string, string>> headerParams = null)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler());
            if (BaseUrl != null)
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (var pair in headerParams)
                {
                    httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }
            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.PostAsync(path, new FormUrlEncodedContent(parameters));
            }
            catch
            {

            }

            var responseString = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.ToString());
            callback(response.IsSuccessStatusCode, Convert.ToInt32(response.StatusCode), json);
        }

        public static async void StartJSONRequestOperationPut(string path, List<KeyValuePair<string, string>> parameters, Action<bool, int, JObject> callback, List<KeyValuePair<string, string>> headerParams = null)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler());
            if (BaseUrl != null)
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (var pair in headerParams)
                {
                    httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }

            HttpResponseMessage response = await httpClient.PutAsync(path, new FormUrlEncodedContent(parameters));

            var responseString = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.ToString());
            callback(response.IsSuccessStatusCode, Convert.ToInt32(response.StatusCode), json);
        }

        public static async void StartJSONRequestOperationGet(string path, List<KeyValuePair<string, string>> parameters, Action<bool, int, JObject> callback, List<KeyValuePair<string, string>> headerParams = null)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler());
            if (BaseUrl != null)
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (var pair in headerParams)
                {
                    httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }

            string getParameters = "";

            if (parameters != null && parameters.Count > 0)
                getParameters = ConvertKeyValuePairToGETString(parameters);

            path += getParameters;

            HttpResponseMessage response = await httpClient.GetAsync(path);

            var responseString = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.ToString());
            callback(response.IsSuccessStatusCode, Convert.ToInt32(response.StatusCode), json);
        }

        public static async void StartJSONRequestOperationDelete(string path, List<KeyValuePair<string, string>> values, Action<bool, int, JObject> callback, List<KeyValuePair<string, string>> headerParams = null)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler());
            if (BaseUrl != null)
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
            }

            if (headerParams != null && headerParams.Count > 0)
            {
                foreach (var pair in headerParams)
                {
                    httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }

            string parameters = "";

            if (values != null && values.Count > 0)
                parameters = ConvertKeyValuePairToGETString(values);

            path += parameters;

            HttpResponseMessage response = await httpClient.DeleteAsync(path);

            var responseString = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.ToString());
            callback(response.IsSuccessStatusCode, Convert.ToInt32(response.StatusCode), json);
        }

        private static string ConvertKeyValuePairToGETString(List<KeyValuePair<string, string>> data)
        {
            if (data.Count == 0)
                return "?";

            string result = "?";
            foreach (var pair in data)
            {
                result += pair.Key.ToString() + "=" + pair.Value.ToString() + "&";
            }

            if (result[result.Length - 1] == '&')
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result;
        }
    }
}
