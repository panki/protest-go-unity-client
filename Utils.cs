using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtestGoClient
{
    static class Utils
    {
        static public long str2unixtime(string v)
        {
            return String.IsNullOrEmpty(v) ? -1 : DateTime.Parse(v).ToFileTimeUtc();
        }

        static public string unixtime2str(long v)
        {
            return DateTime.FromFileTimeUtc(v).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
        }

    }
}