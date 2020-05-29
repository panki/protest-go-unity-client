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
        public class ProtestsConfig
        {
            public List<uint> allowedProtestTypes;
            public uint maxProtestDuration;
            public uint maxPaidByLiberoDuration;
            public float pricePerMinuteInLibero;
            public float pricePerMinuteInReal;
        }

        [Serializable]
        public class LeafletsConfig
        {
            public uint maxLeafletDuration;
            public float real2liberoExchangeRate;
            public float real2orderoExchangeRate;
            public uint rewardForCreateInLibero;
            public uint rewardForSignInLibero;
            public uint rewardForDestroyInOrdero;
        }

        [Serializable]
        public class AccountsConfig
        {
            public float real2liberoExchangeRate;
            public float real2orderoExchangeRate;
        }

        [Serializable]
        public class PurchasesConfig
        {
            public List<CatalogItem> catalog;
        }

        [Serializable]
        public class UrlsConfig
        {
            public string resourcesUrl;
            public string supportEmail;
            public string supportTelegram;
        }

        [Serializable]
        public class RatingsConfig
        {
            public List<RatingsRank> orderRanks;
            public List<RatingsRank> freedomRanks;
        }

        [Serializable]
        public class FeaturesConfig
        {
            public bool leafletsEnabled;
        }

        [Serializable]
        public class Init
        {
            public ProtestsConfig protests;
            public LeafletsConfig leaflets;
            public AccountsConfig accounts;
            public PurchasesConfig purchases;
            public UrlsConfig urls;
            public RatingsConfig ratings;
            public FeaturesConfig features;
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