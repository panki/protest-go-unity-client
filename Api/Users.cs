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
        public class Nickname
        {
            public string nickname;
        }
    }

    namespace Res
    {
        [Serializable]
        public class UserAvatar
        {
            public string id;
            public uint avatarId;
            public string protestId;
            public string nickname;
        }
        [Serializable]
        public class Me
        {
            public string id;
            public string nickname;

            public List<Res.UserAvatar> avatars;

            [SerializeField]
            private string createdAt;

            public DateTime createdDt
            {
                get { return DateTime.Parse(createdAt); }
                set { createdAt = value.ToString(); }
            }
        }
    }

    public static partial class Client
    {
        public static class Users
        {
            /*
            Me - requests current user's information.
            */
            public static IPromise<Res.Me> Me()
            {
                return get<Res.Me>("/users/me");
            }

            public static IPromise<bool> CheckNickname(string nickname)
            {
                Req.Nickname req = new Req.Nickname { nickname = nickname };
                return post<Res.Success>("/users/checkNickname", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> SetNickname(string nickname)
            {
                Req.Nickname req = new Req.Nickname { nickname = nickname };
                return put<Res.Success>("/users/setNickname", req)
                .Then(res => res.success);
            }
        }
    }
}