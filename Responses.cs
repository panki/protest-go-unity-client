using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient.Res
{
    [Serializable]
    class Error
    {
        public uint statusCode;
        public string error;
        public string message;
    }

    [Serializable]
    public class Success
    {
        public bool success;
    }
}

