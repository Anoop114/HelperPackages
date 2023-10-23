using System.Collections;
using HelperFunction.Server;
using UnityEngine;
using UnityEngine.UI;

namespace ServerDemo.Script
{
    public class ServerExample : MonoBehaviour
    {
        #region Variables

        private const string GetPutPatchDeleteAPI = "https://reqres.in/api/users/2";
        private const string PostAPI = "https://reqres.in/api/users";
        
        //-----------------------------------------------------------------------------------//
        [Space,Header("Coroutine")]
        [SerializeField] private Text displayText;
        [SerializeField] private InputField name;
        [SerializeField] private InputField job;
        private ServerCall serverCaller;

        private bool isPutCall;
        private bool isPostCall;
        private bool isAsyncPutCall;
        private bool isAsyncPostCall;
        
        //-----------------------------------------------------------------------------------//
        [Space,Header("Async")]
        [SerializeField] private Text displayAsyncText;
        [SerializeField] private InputField nameAsync;
        [SerializeField] private InputField jobAsync;

        //-----------------------------------------------------------------------------------//
        [Space,Header("Common Data")]
        [SerializeField] private GetApi getApiData;
        [SerializeField] private PutGetData putGetData;
        [SerializeField] private PostGetData postGetData;
        #endregion

        
        private void Start() => serverCaller = new ServerCall();
        private void ResetTextInputField() => name.text = job.text = nameAsync.text = jobAsync.text = "";

        
        #region Coroutine Server Call

        private void UpdateDisplayText(string textData) => displayText.text = textData;


        #region Get

        public void GetCall()
        {
            serverCaller.GetCall(
                GetPutPatchDeleteAPI,
                null,
                UpdateDisplayText,
                OnGetSuccess
            );
        }

        private void OnGetSuccess(string getRespond)
        {
            getApiData = JsonUtility.FromJson<GetApi>(getRespond);
            UpdateDisplayText(getApiData.DisplayAllData());
            ResetTextInputField();

        }

        #endregion

        #region Put

        public void PutCall()
        {
            isPutCall = true;
        }
        
        private void PutCallServer()
        {
            var putData = new PutApiSend()
            {
                name = name.text,
                job = job.text
            };

            var sendData = new Hashtable()
            {
                {ServerDataHolder.MessageCode,JsonUtility.ToJson(putData) }
            };
            
            serverCaller.PutCall(
                GetPutPatchDeleteAPI,
                sendData,
                UpdateDisplayText,
                OnPutSuccess
            );

        }
        private void OnPutSuccess(string putRespond)
        {
            putGetData = JsonUtility.FromJson<PutGetData>(putRespond);
            UpdateDisplayText(putGetData.DisplayAllData());
            ResetTextInputField();

        }

        #endregion

        #region Post

        public void PostCall()
        {
            isPostCall = true;
        }
        
        private void PostCallServer()
        {
            var data = new PostApiSend()
            {
                name = name.text,
                job = job.text
            };

            var sendData = new Hashtable()
            {
                {ServerDataHolder.MessageCode,JsonUtility.ToJson(data) }
            };
            
            serverCaller.PostCall(
                PostAPI,
                sendData,
                UpdateDisplayText,
                OnPostSuccess
            );
        }
        private void OnPostSuccess(string postRespond)
        {
            postGetData = JsonUtility.FromJson<PostGetData>(postRespond);
            UpdateDisplayText(postGetData.DisplayAllData());
            ResetTextInputField();

        }

        #endregion
        public void CommonServerFunctionCall()
        {
            if (isPostCall)
            {
                isPostCall = isPutCall = false;
                PostCallServer();
                return;
            }

            if (isPutCall)
            {
                isPostCall = isPutCall = false;
                PutCallServer();
                return;
            }
            
            return;
        }

        #endregion

        #region Async Server Call
        private void UpdateDisplayTextAsync(string textData) => displayAsyncText.text = textData;
        
        #region Get

        public async void GetAsyncCall()
        {
            var getRequest = await ServerCallAsync.GET_Request(
                GetPutPatchDeleteAPI,
                UpdateDisplayTextAsync
                );
            getApiData = JsonUtility.FromJson<GetApi>(getRequest);
            UpdateDisplayTextAsync(getApiData.DisplayAllData());
            ResetTextInputField();

        }

        #endregion

        #region Put

        public void PutAsyncCaller()
        {
            isAsyncPutCall = true;
        }
        
        private async void PutAsyncCall()
        {
            var putSendData = new PutApiSend()
            {
                name = nameAsync.text,
                job = jobAsync.text
            };
            var sendData = JsonUtility.ToJson(putSendData);
             
            var receiveData = await ServerCallAsync.PUT_Request(GetPutPatchDeleteAPI, sendData, UpdateDisplayTextAsync);

            putGetData = JsonUtility.FromJson<PutGetData>(receiveData);
            UpdateDisplayTextAsync(putGetData.DisplayAllData());
            ResetTextInputField();

        }

        #endregion

        #region Post

        public void PostAsyncCaller()
        {
            isAsyncPostCall = true;
        }

        private async void PostCallAsync()
        {
            var postSendData = new PostApiSend()
            {
                name = nameAsync.text,
                job = jobAsync.text
            };

            var data = JsonUtility.ToJson(postSendData);

            var respond = await ServerCallAsync.POST_Request(PostAPI, data, UpdateDisplayTextAsync);

            var dataReceived = JsonUtility.FromJson<PostGetData>(respond);
            UpdateDisplayTextAsync(dataReceived.DisplayAllData());
            ResetTextInputField();
        }
        
        #endregion
        
        public void CommonAsyncServerFunctionCall()
        {
            if (isAsyncPostCall)
            {
                isAsyncPutCall = isAsyncPostCall = false;
                PostCallAsync();
                return;
            }

            if (isAsyncPutCall)
            {
                isAsyncPutCall = isAsyncPostCall = false;
                PutAsyncCall();
            }
        }
        


        #endregion
        
        
    }
}