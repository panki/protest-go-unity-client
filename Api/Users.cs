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

        public class ChangeEmail
        {
            public string email;
            public string code;
        }

        public class Login
        {
            public string email;
            public string code;
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
        public class Login
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
            public string email;

            public List<Res.UserAvatar> userAvatars;

            public List<Res.Participant> participations;
            public List<Res.Signatory> signatures;
            public List<string> roles; // See UserRole

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
                return post<Res.Login>("/users", new Req.CreateUser { unityId = deviceId })
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

            public static IPromise<bool> SendConfirmationCode(string email)
            {
                Req.Email req = new Req.Email { email = email };
                return put<Res.Success>("/users/sendConfirmationCode", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> CheckEmail(string email)
            {
                Req.Email req = new Req.Email { email = email };
                return post<Res.Success>("/users/checkEmail", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> ChangeEmail(string email, string code)
            {
                Req.ChangeEmail req = new Req.ChangeEmail { email = email, code = code };
                return put<Res.Success>("/users/changeEmail", req)
                .Then(res => res.success);
            }

            public static IPromise<string> Login(string email, string code)
            {
                Req.Login req = new Req.Login { email = email, code = code };
                return put<Res.Login>("/users/login", req)
                .Then(res => res.token);
            }
        }
    }
}