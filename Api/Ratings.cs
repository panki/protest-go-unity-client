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
        public class Top
        {
            public uint start;
            public uint count;
        }
    }

    namespace Res
    {

    }

    public static partial class Client
    {
        public static class Ratings
        {
            public static IPromise<List<Res.User>> TopFreedomUsers(uint start, uint count)
            {
                Req.Top req = new Req.Top { start = start, count = count };
                return post<Res.UsersResponse>("/ratings/topFreedomUsers").Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Users(res.users);
                });
            }

            public static IPromise<List<Res.User>> TopOrderUsers(uint start, uint count)
            {
                Req.Top req = new Req.Top { start = start, count = count };
                return post<Res.UsersResponse>("/ratings/topOrderUsers").Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Users(res.users);
                });
            }
        }
    }
}