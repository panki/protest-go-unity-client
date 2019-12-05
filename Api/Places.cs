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
        public class Place
        {
            public uint id;
            public string symId;
            public float lat;
            public float lon;
            public uint radius;
            public uint modeId;

        }

        [Serializable]
        public class Places
        {
            public List<Place> places;
        }
    }

    public static partial class Client
    {
        public static class Places
        {
            /*
            GetAll - request all places
            */
            public static IPromise<List<Res.Place>> QueryAll()
            {
                return get<Res.Places>("/places").Then(res => res.places);
            }
        }
    }
}