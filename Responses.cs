using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient.Res
{
    [Serializable]
    class Error
    {
        public uint statusCode = 0;
        public string error = "";
        public string message = "";
    }

    [Serializable]
    public class Success
    {
        public bool success;
    }

    public enum ProtestStatus : uint
    {
        ACTIVE = 1,
        LEAVED = 2,
        EXPIRED = 3,
        DISPERSED = 4,
    }

    public enum ParticipantStatus : uint
    {
        ACTIVE = 1,
        LEAVED = 2,
        ARRESTED = 3,
        KICKED = 4,
    }
}

