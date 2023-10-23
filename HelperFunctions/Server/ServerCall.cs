using System;
using System.Collections;
using HelperFunction.Routines;
using UnityEngine.Networking;

namespace HelperFunction.Server
{
    public class ServerCall
    {
        private IEnumerator _serverCallPost;
        private IEnumerator _serverCallPut;
        private IEnumerator _serverCallGet;

        #region PostCall
        public void PostCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {   
            _serverCallPost = POST_Request(url,data,failCall,complete);
            CoroutineManager.Instance.StartRoutine(_serverCallPost);
        }
        
        public void StopPostCall()
        {
            if(_serverCallPost != null) { CoroutineManager.Instance.StopRoutine(_serverCallPost); }
        }


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
        public void PutCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {
            if(_serverCallPut != null) { CoroutineManager.Instance.StartRoutine(_serverCallPut); }
            _serverCallPut = PUT_Request(url, data, failCall, complete);
            CoroutineManager.Instance.StartRoutine(_serverCallPut);
        }
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
        public void GetCall(string url, Hashtable data, Action<string> failCall, Action<string> complete)
        {
            
            _serverCallGet = GET_Request(url, data, failCall, complete);
            CoroutineManager.Instance.StartRoutine(_serverCallGet);
        }

        public void StopGetCall()
        {
            if (_serverCallGet != null) { CoroutineManager.Instance.StopRoutine(_serverCallGet); }
        }

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
                        case ServerDataHolder.BasicAuth:
                            request?.SetRequestHeader("Authorization", "Basic " + pair.Value);
                            break;
                        case ServerDataHolder.BearerAuth:
                            request?.SetRequestHeader("Authorization", "Bearer " + pair.Value);
                            break;
                        case ServerDataHolder.DigestAuth:
                            request?.SetRequestHeader("Authorization", "Digest " + pair.Value);
                            break;
                        case ServerDataHolder.HawkAuth:
                            request?.SetRequestHeader("Authorization", "Hawk " + pair.Value);
                            break;
                        case ServerDataHolder.AwsSignature:
                            request?.SetRequestHeader("Authorization", "AWS4-HMAC-SHA256 " + pair.Value);
                            break;
                        case ServerDataHolder.ApiKey:
                            request?.SetRequestHeader("X-API-Key", pair.Value.ToString());
                            break;
                        case ServerDataHolder.MessageCode:
                            msgToSend = pair.Value.ToString();
                            break;
                    }
                }   

                if (msgToSend != "")
                {
                    var jsonToSend = new System.Text.UTF8Encoding().GetBytes(msgToSend);
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