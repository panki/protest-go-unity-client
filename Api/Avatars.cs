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
        public class Avatar
        {
            public uint id;
            public string symId;
            public uint type; // 1 - initial, 2 - onsell

            public string sex; // M - male, F - female
            public string nickname;
        }

        [Serializable]
        public class AvatarsResponse
        {
            public List<Avatar> avatars;
            public Graph graph;
        }
    }
    public static partial class Client
    {
        public static class Avatars
        {
            public static IPromise<List<Res.Avatar>> QueryAll()
            {
                return get<Res.AvatarsResponse>("/avatars").Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Avatars(res.avatars);
                });
            }

            public static IPromise<List<Res.Avatar>> QueryInitial()
            {
                return get<Res.AvatarsResponse>("/avatars/initial").Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Avatars(res.avatars);
                });
            }
        }
    }
}