using RSG;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ProtestGoClient
{
    namespace Res
    {
        [Serializable]
        public class Init
        {
            public string resourcesUrl;
            public List<uint> allowedProtestTypes;
            public uint maxProtestDuration;
            public uint maxPaidByLiberoDuration;
            public float pricePerMinuteInLibero;
            public float pricePerMinuteInReal;
            public uint maxLeafletDuration;
            public float real2liberoExchangeRate;
            public float real2orderoExchangeRate;
            public List<CatalogItem> purchasesCatalog;
        }
    }

    public static partial class Client
    {
        public static class App
        {
            /*
            Init - requests init configuration from server
            */
            public static IPromise<Res.Init> Init()
            {
                return get<Res.Init>("/init");
            }
        }
    }
}