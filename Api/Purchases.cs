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
        public class CatalogItem
        {
            public string id;
            public uint realAmount;
            public uint liberoAmount;
            public uint orderoAmount;
        }

        [Serializable]
        public class Catalog
        {
            public List<CatalogItem> catalog;
        }
    }

    namespace Req
    {
        [Serializable]
        public class VerifyRequest
        {
            public string receipt;
        }
    }

    public static partial class Client
    {
        public static class Purchases
        {
            public static IPromise<List<Res.CatalogItem>> GetCatalog()
            {
                return get<Res.Catalog>("/purchases/catalog").Then(res => res.catalog);
            }

            public static IPromise<Res.Account> Verify(string receipt)
            {
                Req.VerifyRequest req = new Req.VerifyRequest { receipt = receipt };
                return post<Res.AccountResponse>("/purchases/verify", req).Then(res => res.account);
            }
        }
    }
}