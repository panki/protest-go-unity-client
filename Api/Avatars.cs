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
            public uint type;
        }

        [Serializable]
        public class Avatars
        {
            public List<Avatar> avatars;
        }
    }
    public static partial class Client
    {
        public static class Avatars
        {
            /*
            GetAll - request all avatars on sale
            */
            public static IPromise<List<Res.Avatar>> GetAllOnSale()
            {
                return get<Res.Avatars>("/avatars").Then(res => res.avatars);
            }
        }
    }
}