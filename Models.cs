using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient
{
    [Serializable]
    class ErrorResponse
    {
        public int statusCode;
        public string error;
        public string message;
    }

    [Serializable]
    public class RecordInitResponse
    {
        public string resourcesUrl;
    }

    [Serializable]
    public class RecordRegisterRequest
    {
        public string unityId;
    }

    [Serializable]
    public class RecordRegisterResponse
    {
        public string token;
    }

    [Serializable]
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

    [Serializable]
    public class RecordNicknameRequest
    {
        public string nickname;
    }

    [Serializable]
    public class RecordSuccessResponse
    {
        public bool success;
    }

    [Serializable]
    public class RecordPlaceResponse
    {
        public int id;
        public string symId;
        public float lat;
        public float lon;
        public int radius;
        public int modeId;

    }

    [Serializable]
    public class RecordPlacesResponse
    {
        public List<RecordPlaceResponse> places;
    }
}

