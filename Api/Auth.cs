using RSG;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ProtestGoClient
{
    namespace Req
    {

        public class Login
        {
            public string email;
            public string code;
        }

        [Serializable]
        public class Signup
        {
            public string unityId;
        }
    }

    namespace Res
    {
        [Serializable]
        public class Login
        {
            public string token;
        }
    }

    public static partial class Client
    {
        public static class Auth
        {
            public static IPromise<string> Signup()
            {
                Req.Signup req = new Req.Signup { unityId = deviceId };
                return post<Res.Login>("/auth/signup", req)
                .Then(res => SetAccessToken(res.token));
            }

            public static IPromise<string> Login(string email, string code)
            {
                Req.Login req = new Req.Login { email = email, code = code };
                return put<Res.Login>("/auth/login", req)
                .Then(res => SetAccessToken(res.token));
            }

            public static IPromise<bool> Logout()
            {
                return put<Res.Login>("/auth/logout", new Req.Signup { unityId = deviceId })
                .Then(res => SetAccessToken(null))
                .Then(res => true);
            }

            public static IPromise<bool> SendOTP(string email)
            {
                Req.Email req = new Req.Email { email = email };
                return put<Res.Success>("/auth/sendOTP", req)
                .Then(res => res.success);
            }
        }
    }
}