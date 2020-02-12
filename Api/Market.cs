using RSG;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ProtestGoClient
{
    namespace Req
    {
        [Serializable]
        public class BuyAvatar
        {
            public string id;
            public string nickname;
        }
    }

    namespace Res
    {

        [Serializable]
        public class BuyAvatar
        {
            public UserAvatar userAvatar;
            public Account account;
        }

        [Serializable]
        public class MarketAvatar
        {
            public string id;
            public string symId;
            public uint avatarId;
            public string currency;
            public uint price;
            public List<uint> placeIds;
        }

        [Serializable]
        public class MarketCatalog
        {
            public List<MarketAvatar> avatars;
        }
    }

    public static partial class Client
    {
        public static class Market
        {
            public static IPromise<Res.MarketCatalog> GlobalCatalog()
            {
                return get<Res.MarketCatalog>("/market/catalog");
            }

            public static IPromise<Res.MarketCatalog> PlaceCatalog(uint placeId)
            {
                return get<Res.MarketCatalog>("/market/place/" + placeId.ToString() + "/catalog");
            }

            public static IPromise<Res.BuyAvatar> BuyAvatar(string id, string nickname = "")
            {
                Req.BuyAvatar req = new Req.BuyAvatar { id = id, nickname = nickname };
                return post<Res.BuyAvatar>("/market/avatars/buy", req);
            }
        }
    }
}