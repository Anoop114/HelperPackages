using System;

namespace ServerDemo.Script
{
    #region Get

    [Serializable]
    public struct GetApi
    {
        public Data data;
        public Support support;
        
        [Serializable]
        public struct Data
        {
            public int id;
            public string email;
            public string avatar;
            public string first_name;
            public string last_name;
        }

        [Serializable]
        public struct Support
        {
            public string text;
            public string url;
        }

        public string DisplayAllData()
        {
            var dataReturn = $"Data \n \nid : {data.id}" +
                             $"\nemail : {data.email}" +
                             $"\navatar : {data.avatar}" +
                             $"\nfirst_name : {data.first_name}" +
                             $"\nlast_name : {data.last_name}";

            var supportReturn = $"Support \n \ntext : {support.text}" +
                                $"\nurl : {support.url} \n";

            return dataReturn + "\n \n" + supportReturn;
        }
    }

        #endregion

    #region Put

    [Serializable]
    public struct PutApiSend
    {
        public string name; 
        public string job;

        public PutApiSend(string _name, string _job)
        {
            name = _name;
            job = _job;
        }
    }

    [Serializable]
    public struct PutGetData
    {
        public string name;
        public string job;
        public string updatedAt;

        public string DisplayAllData()
        {
            var sendPutData = $"Put Data \n \n" +
                              $"name : {name}\n" +
                              $"job : {job}\n" +
                              $"Update At : {updatedAt}\n";
            return sendPutData;
        }
    }
    #endregion

    #region Post

    [Serializable]
    public struct PostApiSend
    {
        public string name; 
        public string job;

        public PostApiSend(string _name, string _job)
        {
            name = _name;
            job = _job;
        }
    }

    [Serializable]
    public struct PostGetData
    {
        public string name;
        public string job;
        public int id;
        public string createdAt;

        public string DisplayAllData()
        {
            var sendPostData = $"Post Data \n \n" +
                              $"name : {name}\n" +
                              $"job : {job}\n" +
                              $"id : {id}\n" +
                              $"Created At : {createdAt}\n";
            return sendPostData;
        }
    }
    #endregion
}