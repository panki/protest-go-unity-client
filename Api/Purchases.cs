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
            public string transactionId;
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

            public static IPromise<bool> Verify(string receipt, string transactionId)
            {
                Req.VerifyRequest req = new Req.VerifyRequest { receipt = receipt, transactionId = transactionId };
                return post<Res.Success>("/purchases/verify", req).Then(res => res.success);
            }
        }
    }
}