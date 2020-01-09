using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient.Req
{
    [Serializable]
    public class QueryByIds
    {
        public List<string> ids;
    }
}