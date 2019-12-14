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

            public DateTime startedDt
            {
                get { return DateTime.Parse(startedAt); }
                set { startedAt = value.ToString(); }
            }

            public DateTime finishesDt
            {
                get { return DateTime.Parse(finishesAt); }
                set { finishesAt = value.ToString(); }
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

            public UserAvatar userAvatar;


            [SerializeField]
            private string joinedAt;

            // calculated

            public DateTime joinedDt
            {
                get { return DateTime.Parse(joinedAt); }
                set { joinedAt = value.ToString(); }
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
                string userAvatarId,
                string bannerId,
                List<string> bannerWords,
                bool authorized,
                uint duration)
            {
                Req.CreateProtest req = new Req.CreateProtest
                {
                    placeId = placeId,
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
        }
    }
}