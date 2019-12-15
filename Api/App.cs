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
        public class Register
        {
            public string unityId;
        }
    }

    namespace Res
    {
        [Serializable]
        public class Init
        {
            public string resourcesUrl;
            public List<uint> allowedProtestTypes;
        }

        [Serializable]
        public class Register
        {
            public string token;
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

            /*
            Register - registers new anonymous user,
            returns access token for future requests.
            */
            public static IPromise<string> Register()
            {
                return post<Res.Register>("/register", new Req.Register { unityId = deviceId })
                .Then(res =>
                {
                    accessToken = res.token;
                    return accessToken;
                });
            }
        }
    }
}