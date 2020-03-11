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
        public class Email
        {
            public string email;
        }

        public class SetEmail
        {
            public string email;
            public string code;
        }
    }

    namespace Res
    {
        [Serializable]
        public class RatingValue
        {
            public uint type; // Protest type id
            public int value;
        }

        [Serializable]
        public class Rating
        {
            public int freedom;
            public int freedomPos;
            public int order;
            public int orderPos;
            public List<RatingValue> ratings;
        }

        [Serializable]
        public class UserAvatar
        {
            public string id;
            public uint avatarId;
            public string nickname;
            public string userNickname;
            public uint status;
            public Res.Rating rating;

            [SerializeField]
            private string statusExpiresAt;
            public long statusExpiresDt
            {
                get { return Utils.str2unixtime(statusExpiresAt); }
                set { statusExpiresAt = Utils.unixtime2str(value); }
            }

            [System.NonSerialized]
            public Avatar avatar;
        }

        [Serializable]
        public class User
        {
            public string id;
            public string nickname;
            public string email;

            public Res.Account account;
            public List<Res.UserAvatar> userAvatars;
            public List<Res.Participant> participations;
            public List<Res.Signatory> signatures;
            public List<string> roles; // See UserRole
            public Res.Rating rating;

            [SerializeField]
            private string createdAt;

            public long createdDt
            {
                get { return Utils.str2unixtime(createdAt); }
                set { createdAt = Utils.unixtime2str(value); }
            }

            [System.NonSerialized]
            public bool isDeveloper;
        }

        [Serializable]
        public class MeResponse
        {
            public User user;
            public Graph graph;
        }

        [Serializable]
        public class UsersResponse
        {
            public List<User> users;
            public Graph graph;
        }
    }

    public static partial class Client
    {
        public static class Users
        {
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

            public static IPromise<bool> CheckEmail(string email)
            {
                Req.Email req = new Req.Email { email = email };
                return post<Res.Success>("/users/checkEmail", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> SetEmail(string email, string code)
            {
                Req.SetEmail req = new Req.SetEmail { email = email, code = code };
                return put<Res.Success>("/users/setEmail", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> SendOTP(string email)
            {
                Req.Email req = new Req.Email { email = email };
                return put<Res.Success>("/users/sendOTP", req)
                .Then(res => res.success);
            }
        }
    }
}