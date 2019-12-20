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

        [Serializable]
        public class CreateUser
        {
            public string unityId;
        }
    }

    namespace Res
    {
        [Serializable]
        public class CreateUser
        {
            public string token;
        }

        [Serializable]
        public class UserAvatar
        {
            public string id;
            public uint avatarId;
            public string nickname;
            public string userNickname;

            [System.NonSerialized]
            public Avatar avatar;
        }
        [Serializable]
        public class User
        {
            public string id;
            public string nickname;

            public List<Res.UserAvatar> userAvatars;

            public List<Res.Participant> participations;

            [SerializeField]
            private string createdAt;

            public long createdDt
            {
                get { return DateTime.Parse(createdAt).ToFileTimeUtc(); }
                set { createdAt = DateTime.FromFileTimeUtc(value).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); }
            }
        }

        [Serializable]
        public class MeResponse
        {
            public User user;
            public Graph graph;
        }
    }

    public static partial class Client
    {
        public static class Users
        {
            /*
            Register - registers new anonymous user,
            returns access token for future requests.
            */
            public static IPromise<string> Create()
            {
                return post<Res.CreateUser>("/create", new Req.CreateUser { unityId = deviceId })
                .Then(res =>
                {
                    accessToken = res.token;
                    return accessToken;
                });
            }
            /*
            Me - requests current user's information.
            */
            public static IPromise<Res.User> Me()
            {
                return get<Res.MeResponse>("/users/me").Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.User(res.user);
                });
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