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
        public class Banner
        {
            public string id;
            public string symId;
            public uint protestTypeId;
            public string template;
            public List<string> words;
            public string language;
        }

        [Serializable]
        public class Banners
        {
            public List<Banner> banners;
        }
    }

    public static partial class Client
    {
        public static class Banners
        {
            public static IPromise<List<Res.Banner>> QueryAll(uint protestTypeId, string language)
            {
                Dictionary<string, string> args = new Dictionary<string, string>{
                    { "protestTypeId", protestTypeId.ToString() },
                    { "language", language }
                };
                return get<Res.Banners>("/banners", args)
                .Then(res => res.banners);
            }
        }
    }
}