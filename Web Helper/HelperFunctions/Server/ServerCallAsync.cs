using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace HelperFunction.Server
{
    /// <summary>
    /// async await fun call
    /// </summary>
    public static class ServerCallAsync
    {
        #region ServerCall Public function

        public static async Task<string> GET_Request(string url, Action<string> errorCall = null)
        {
            using var request = new UnityWebRequest(url, "GET");
            return await SendAndReceiveDataFromServer(null,errorCall, request);
        }
        public static async Task<string> POST_Request(string url, string jsonString, Action<string> errorCall = null)
        {
            using var request = new UnityWebRequest(url, "POST");
            return await SendAndReceiveDataFromServer(jsonString,errorCall, request);
        }
        public static async Task<string> PUT_Request(string url, string jsonString, Action<string> errorCall = null)
        {
            using var request = new UnityWebRequest(url, "PUT");
            return await SendAndReceiveDataFromServer(jsonString,errorCall, request);
        }
        

        #endregion
        
        private static async Task<string> SendAndReceiveDataFromServer(string sendData ,Action<string> errorCall, UnityWebRequest request)
        {
            if (!string.IsNullOrEmpty(sendData))
            {
                var jsonToSend = new System.Text.UTF8Encoding().GetBytes(sendData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
            
            while (!request.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.ConnectionError) return request.downloadHandler.text;
            errorCall?.Invoke(request.result.ToString());
            return request.error;
        }
    }
}