using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace WebHelper
{
    public static class WebCallAsync
    {
        public static async Task<string> GetAsync(string baseUrl,Action<string> error,string authorization = null)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            using var response = await httpClient.GetAsync(baseUrl);

            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    error?.Invoke($"Error: {response.StatusCode}");
                    return "";
                }
            
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException  e)
            {
                error?.Invoke($"Error: {e.Message}");
                return "";
            }
            catch (TaskCanceledException e)
            {
                // Handle timeouts (TaskCanceledException is thrown when the request times out)
                error?.Invoke(e.CancellationToken.IsCancellationRequested
                    ? "Request was canceled."
                    : "Request timed out.");
                return "";
            }
            catch (Exception e)
            {
                error?.Invoke($"An error occurred: {e.Message}");
                return "";
            }
        }

        public static async Task<string> PostAsync(string baseUrl, string data, Action<string> error,string authorization = null)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            using var response = await httpClient.PostAsync(baseUrl, content);

            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    error?.Invoke($"Error: {response.StatusCode}");
                    return "";
                }
            
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (HttpRequestException  e)
            {
                error?.Invoke($"Error: {e.Message}");
                return "";
            }
            catch (TaskCanceledException e)
            {
                // Handle timeouts (TaskCanceledException is thrown when the request times out)
                error?.Invoke(e.CancellationToken.IsCancellationRequested
                    ? "Request was canceled."
                    : "Request timed out.");
                return "";
            }
            catch (Exception e)
            {
                error?.Invoke($"An error occurred: {e.Message}");
                return "";
            }
        }

        public static async Task<string> PutAsync(string baseUrl, string data, Action<string> error,string authorization = null)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            using var response = await httpClient.PutAsync(baseUrl, content);
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    error?.Invoke($"Error: {response.StatusCode}");
                    return "";
                }

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (HttpRequestException e)
            {
                error?.Invoke($"Error: {e.Message}");
                return "";
            }
            catch (TaskCanceledException e)
            {
                // Handle timeouts (TaskCanceledException is thrown when the request times out)
                error?.Invoke(e.CancellationToken.IsCancellationRequested
                    ? "Request was canceled."
                    : "Request timed out.");
                return "";
            }
            catch (Exception e)
            {
                error?.Invoke($"An error occurred: {e.Message}");
                return "";
            }
        }

        public static async Task<string> PatchAsync(string baseUrl, string data, Action<string> error, string authorization = null)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), baseUrl) { Content = content };
            using var response = await httpClient.SendAsync(request);
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    error?.Invoke($"Error: {response.StatusCode}");
                    return "";
                }

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (HttpRequestException e)
            {
                error?.Invoke($"Error: {e.Message}");
                return "";
            }
            catch (TaskCanceledException e)
            {
                // Handle timeouts (TaskCanceledException is thrown when the request times out)
                error?.Invoke(e.CancellationToken.IsCancellationRequested
                    ? "Request was canceled."
                    : "Request timed out.");
                return "";
            }
            catch (Exception e)
            {
                error?.Invoke($"An error occurred: {e.Message}");
                return "";
            }
        }

        public static async Task<string> DeleteAsync(string baseUrl, Action<string> error, string authorization = null)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",authorization);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            using var response = await httpClient.DeleteAsync(baseUrl);

            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    error?.Invoke($"Error: {response.StatusCode}");
                    return "";
                }

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException e)
            {
                error?.Invoke($"Error: {e.Message}");
                return "";
            }
            catch (TaskCanceledException e)
            {
                error?.Invoke(e.CancellationToken.IsCancellationRequested
                    ? "Request was canceled."
                    : "Request timed out.");
                return "";
            }
            catch (Exception e)
            {
                error?.Invoke($"An error occurred: {e.Message}");
                return "";
            }
        }
    }

    public class WebCallCoroutine
    {
        private IEnumerator _serverCallPost;
        private IEnumerator _serverCallPut;
        private IEnumerator _serverCallGet;

        private static class WebAuth
        {
            //Token
            public const string BasicAuth = "BasicAuth";
            public const string BearerAuth = "BearerAuth";
            public const string DigestAuth = "DigestAuth";
            public const string HawkAuth = "HawkAuth";
            public const string AwsSignature = "AwsSignature";
        
            //msg
            public const string MessageCode = "MsgCode";
        
            //api key's
            public const string ApiKey = "APiKey";
            //public const string JsonWebTokens  = "JWT";
        }
    

        #region PostCall
        // public  void PostCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        // {   
        //     _serverCallPost = POST_Request(url,data,failCall,complete);
        //     CoroutineManager.Instance.StartRoutine(_serverCallPost);
        // }
        //
        // public void StopPostCall()
        // {
        //     if(_serverCallPost != null) { CoroutineManager.Instance.StopRoutine(_serverCallPost); }
        // }


        private static IEnumerator POST_Request(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {
            using var request = new UnityWebRequest(url, "POST");
            PostHeaderSender(data, request);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                failCall?.Invoke(request.result.ToString());
            }
            else
            {
                var status = request.downloadHandler.text;
                complete(status);
            }
        }
        #endregion
     
        #region PUTCall
        // public void PutCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        // {
        //     if(_serverCallPut != null) { CoroutineManager.Instance.StartRoutine(_serverCallPut); }
        //     _serverCallPut = PUT_Request(url, data, failCall, complete);
        //     CoroutineManager.Instance.StartRoutine(_serverCallPut);
        // }
        private static IEnumerator PUT_Request(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {
            using var request = new UnityWebRequest(url, "PUT");
            PostHeaderSender(data, request);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                failCall?.Invoke(request.result.ToString());
            }
            else
            {
                var status = request.downloadHandler.text;
                complete(status);
            }
        }
        #endregion
    
        #region GETCall
        // public void GetCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        // {
        //     
        //     _serverCallGet = GET_Request(url, data, failCall, complete);
        //     CoroutineManager.Instance.StartRoutine(_serverCallGet);
        // }
        //
        // public void StopGetCall()
        // {
        //     if (_serverCallGet != null) { CoroutineManager.Instance.StopRoutine(_serverCallGet); }
        // }

        private static IEnumerator GET_Request(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {
            using var request = new UnityWebRequest(url, "GET");
            PostHeaderSender(data, request);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                failCall?.Invoke(request.result.ToString());
            }
            else
            {
                var status = request.downloadHandler.text;
                complete(status);
            }
        }
        #endregion

        #region Header
        private static void PostHeaderSender(Hashtable data, UnityWebRequest request = null)
        {
            var msgToSend = "";
            if(data != null)
            {
                foreach (DictionaryEntry pair in data)
                {
                    switch (pair.Key)
                    {
                        case WebAuth.BasicAuth:
                            request?.SetRequestHeader("Authorization", "Basic " + pair.Value);
                            break;
                        case WebAuth.BearerAuth:
                            request?.SetRequestHeader("Authorization", "Bearer " + pair.Value);
                            break;
                        case WebAuth.DigestAuth:
                            request?.SetRequestHeader("Authorization", "Digest " + pair.Value);
                            break;
                        case WebAuth.HawkAuth:
                            request?.SetRequestHeader("Authorization", "Hawk " + pair.Value);
                            break;
                        case WebAuth.AwsSignature:
                            request?.SetRequestHeader("Authorization", "AWS4-HMAC-SHA256 " + pair.Value);
                            break;
                        case WebAuth.ApiKey:
                            request?.SetRequestHeader("X-API-Key", pair.Value.ToString());
                            break;
                        case WebAuth.MessageCode:
                            msgToSend = pair.Value.ToString();
                            break;
                    }
                }   

                if (msgToSend != "")
                {
                    var jsonToSend = new UTF8Encoding().GetBytes(msgToSend);
                    if (request != null) request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                }
            }

            if (request == null) return;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
        }
        #endregion
    }
}