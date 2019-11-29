using UnityEngine;
using System;

namespace ProtestGoClient
{
    class ErrorResponse
    {
        public int statusCode;
        public string error;
        public string message;
    }

    public class RecordInitRequest { }

    public class RecordInitResponse
    {
        public string resourcesUrl;
    }

    public class RecordRegisterRequest
    {
        public string unityId;
    }

    public class RecordRegisterResponse
    {
        public string token;
    }

    public class RecordMeResponse
    {
        public string id;
        public string nickname;

        [SerializeField]
        private string createdAt;

        public DateTime createdDt
        {
            get { return DateTime.Parse(createdAt); }
            set { createdAt = value.ToString(); }
        }
    }

    public class RecordNicknameRequest
    {
        public string nickname;
    }

    public class RecordSuccessResponse
    {
        public bool success;
    }
}

