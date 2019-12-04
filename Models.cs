using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient
{
    [Serializable]
    class ErrorResponse
    {
        public uint statusCode;
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

        public List<RecordAvatarResponse> avatars;

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
        public uint id;
        public string symId;
        public float lat;
        public float lon;
        public uint radius;
        public uint modeId;

    }

    [Serializable]
    public class RecordPlacesResponse
    {
        public List<RecordPlaceResponse> places;
    }

    [Serializable]
    public class RecordAvatarResponse
    {
        public uint id;
        public string symId;
        public uint type;
    }

    [Serializable]
    public class RecordAvatarsResponse
    {
        public List<RecordAvatarResponse> avatars;
    }
}

