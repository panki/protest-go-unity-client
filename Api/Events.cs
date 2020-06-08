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
        public class Event
        {
            public uint id;
            public string type;
            public bool seen;
            public string userId;
            public string protestId;
            public string participantId;
            public Res.RatingValue rating;
            public Res.Money money;

            [SerializeField]
            private string createdAt;

            // calculated

            public long createdDt
            {
                get { return Utils.str2unixtime(createdAt); }
                set { createdAt = Utils.unixtime2str(value); }
            }

            // Relations

            [System.NonSerialized]
            public Res.Protest protest;

            [System.NonSerialized]
            public Res.Participant participant;
        }

        [Serializable]
        class EventsResponse
        {
            public List<Event> events = null;
            public Graph graph = null;
        }
    }

    public static partial class Client
    {
        public static class Events
        {
            public static IPromise<List<Res.Event>> QueryNotSeen()
            {
                return get<Res.EventsResponse>("/events")
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Events(res.events);
                });
            }

            public static IPromise<bool> MarkAsSeen(List<uint> ids)
            {
                Req.QueryByIntIds req = new Req.QueryByIntIds { ids = ids };
                return post<Res.Success>("/events/markAsSeen", req)
                .Then(res => res.success);
            }
        }
    }
}