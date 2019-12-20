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

            [System.NonSerialized]
            public Place place;

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
        public class Participant
        {
            public string id;
            public string protestId;
            public string userAvatarId;
            public string bannerId;
            public string bannerText;
            public uint status;

            [System.NonSerialized]
            public UserAvatar userAvatar;

            [System.NonSerialized]
            public Protest protest;


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
        class ParticipantsResponse
        {
            public List<Participant> participants = null;
            public Graph graph = null;
        }

        [Serializable]
        class ParticipantResponse
        {
            public Participant participant = null;
            public Graph graph = null;
        }

        [Serializable]
        class ProtestsResponse
        {
            public List<Protest> protests = null;
            public Graph graph = null;
        }

        [Serializable]
        class ProtestResponse
        {
            public Protest protest = null;
            public Graph graph = null;
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
                return get<Res.ProtestResponse>("/protests/" + id)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Protest(res.protest);
                });
            }

            public static IPromise<List<Res.Protest>> QueryByIds(List<string> ids)
            {
                Req.QueryByIds req = new Req.QueryByIds { ids = ids };
                return post<Res.ProtestsResponse>("/protests/queryByIds", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Protests(res.protests);
                });
            }

            public static IPromise<List<Res.Participant>> QueryParticipants(string protestId)
            {
                return get<Res.ParticipantsResponse>("/protests/" + protestId + "/participants")
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Participants(res.participants);
                });
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
                return post<Res.ProtestResponse>("/protests", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Protest(res.protest);
                });
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
                return post<Res.ParticipantResponse>("/protests/" + protestId + "/join", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Participant(res.participant);
                });
            }

            public static IPromise<Res.Participant> LeaveProtest(
                string protestId,
                string userAvatarId)
            {
                Req.LeaveProtest req = new Req.LeaveProtest
                {
                    userAvatarId = userAvatarId,
                };
                return post<Res.ParticipantResponse>("/protests/" + protestId + "/leave", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Participant(res.participant);
                });
            }
        }
    }
}