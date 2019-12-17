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
        public class Protest
        {
            public string id;
            public uint placeId;
            public uint protestModeId;
            public uint protestTypeId;
            public string organizerId;
            public uint participantsCount;
            public bool authorized;
            public bool finished;

            [SerializeField]
            private string startedAt;

            [SerializeField]
            private string finishesAt;

            // calculated

            public long startedDt
            {
                get { return DateTime.Parse(startedAt).ToFileTimeUtc(); }
                set { startedAt = DateTime.FromFileTimeUtc(value).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); }
            }

            public long finishesDt
            {
                get { return DateTime.Parse(finishesAt).ToFileTimeUtc(); }
                set { finishesAt = DateTime.FromFileTimeUtc(value).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); }
            }
        }

        [Serializable]
        public class Protests
        {
            public List<Protest> protests;
        }

        [Serializable]
        public class Participant
        {
            public string id;
            public string protestId;
            public string userAvatarId;
            public string bannerId;
            public string bannerText;
            public uint status;

            public UserAvatar userAvatar;


            [SerializeField]
            private string joinedAt;

            [SerializeField]
            private string leavedAt;

            // calculated

            public long joinedDt
            {
                get { return DateTime.Parse(joinedAt).ToFileTimeUtc(); }
                set { joinedAt = DateTime.FromFileTimeUtc(value).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); }
            }

            public long leavedDt
            {
                get { return DateTime.Parse(leavedAt).ToFileTimeUtc(); }
                set { leavedAt = DateTime.FromFileTimeUtc(value).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"); }
            }

        }

        [Serializable]
        public class Participants
        {
            public List<Participant> participants;
        }
    }

    namespace Req
    {
        [Serializable]
        public class QueryByIds
        {
            public List<string> ids;
        }

        [Serializable]
        public class CreateProtest
        {
            public uint placeId;
            public uint protestTypeId;
            public string userAvatarId;
            public string bannerId;
            public List<string> bannerWords;
            public bool authorized;
            public uint duration; // in minutes
        }

        [Serializable]
        public class JoinProtest
        {
            public string userAvatarId;
            public string bannerId;
            public List<string> bannerWords;
        }

        public class LeaveProtest
        {
            public string userAvatarId;
        }
    }

    public static partial class Client
    {
        public static class Protests
        {
            public static IPromise<Res.Protest> GetById(string id)
            {
                return get<Res.Protest>("/protests/" + id);
            }

            public static IPromise<List<Res.Protest>> QueryByIds(List<string> ids)
            {
                Req.QueryByIds req = new Req.QueryByIds { ids = ids };
                return post<Res.Protests>("/protests/queryByIds", req)
                .Then(res => res.protests);
            }

            public static IPromise<List<Res.Participant>> QueryParticipants(string protestId)
            {
                return get<Res.Participants>("/protests/" + protestId + "/participants")
                .Then(res => res.participants);
            }

            public static IPromise<Res.Protest> CreateProtest(
                uint placeId,
                uint protestTypeId,
                string userAvatarId,
                string bannerId,
                List<string> bannerWords,
                bool authorized,
                uint duration)
            {
                Req.CreateProtest req = new Req.CreateProtest
                {
                    placeId = placeId,
                    protestTypeId = protestTypeId,
                    userAvatarId = userAvatarId,
                    bannerId = bannerId,
                    bannerWords = bannerWords,
                    authorized = authorized,
                    duration = duration
                };
                return post<Res.Protest>("/protests", req);
            }

            public static IPromise<Res.Participant> JoinProtest(
                string protestId,
                string userAvatarId,
                string bannerId,
                List<string> bannerWords)
            {
                Req.JoinProtest req = new Req.JoinProtest
                {
                    userAvatarId = userAvatarId,
                    bannerId = bannerId,
                    bannerWords = bannerWords,
                };
                return post<Res.Participant>("/protests/" + protestId + "/join", req);
            }

            public static IPromise<Res.Participant> LeaveProtest(
                string protestId,
                string userAvatarId)
            {
                Req.LeaveProtest req = new Req.LeaveProtest
                {
                    userAvatarId = userAvatarId,
                };
                return post<Res.Participant>("/protests/" + protestId + "/leave", req);
            }
        }
    }
}