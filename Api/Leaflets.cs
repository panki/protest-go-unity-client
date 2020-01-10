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
        public class Leaflet
        {
            public string id;
            public uint placeId;
            public uint protestTypeId;
            public string organizerId;
            public uint signatoriesCount;
            public uint maxSignatoriesCount;
            public uint totalSignatoriesCount;
            public bool finished;
            public LeafletStatus status;


            [SerializeField]
            private string startedAt;

            [SerializeField]
            private string finishesAt;

            [SerializeField]
            private string finishedAt;

            [System.NonSerialized]
            public Place place;

            [System.NonSerialized]
            public User organizer;

            // calculated

            public long startedDt
            {
                get { return Utils.str2unixtime(startedAt); }
                set { startedAt = Utils.unixtime2str(value); }
            }

            public long finishesDt
            {
                get { return Utils.str2unixtime(finishesAt); }
                set { finishesAt = Utils.unixtime2str(value); }
            }

            public long finishedDt
            {
                get { return Utils.str2unixtime(finishedAt); }
                set { finishedAt = Utils.unixtime2str(value); }
            }
        }

        [Serializable]
        public class Signatory
        {
            public string id;
            public string leafletId;
            public string userId;
            public string bannerId;
            public string bannerText;
            public SignatoryStatus status;

            [System.NonSerialized]
            public Leaflet leaflet;


            [SerializeField]
            private string signedAt;

            [SerializeField]
            private string destroyedAt;

            [System.NonSerialized]
            public User user;

            // calculated

            public long signedDt
            {
                get { return Utils.str2unixtime(signedAt); }
                set { signedAt = Utils.unixtime2str(value); }
            }

            public long destroyedDt
            {
                get { return Utils.str2unixtime(destroyedAt); }
                set { destroyedAt = Utils.unixtime2str(value); }
            }

        }

        [Serializable]
        class SignatoriesResponse
        {
            public List<Signatory> signatories = null;
            public Graph graph = null;
        }

        [Serializable]
        class SignatoryResponse
        {
            public Signatory signatory = null;
            public Graph graph = null;
        }

        [Serializable]
        class LeafletsResponse
        {
            public List<Leaflet> leaflets = null;
            public Graph graph = null;
        }

        [Serializable]
        class LeafletResponse
        {
            public Leaflet leaflet = null;
            public Graph graph = null;
        }
    }

    namespace Req
    {
        [Serializable]
        public class CreateLeaflet
        {
            public uint placeId;
            public uint protestTypeId;
            public string bannerId;
            public List<string> bannerWords;
        }

        [Serializable]
        public class SignLeaflet
        {
            public string bannerId;
            public List<string> bannerWords;
        }
    }

    public static partial class Client
    {
        public static class Leaflets
        {
            public static IPromise<Res.Leaflet> GetById(string id)
            {
                return get<Res.LeafletResponse>("/leaflets/" + id)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Leaflet(res.leaflet);
                });
            }

            public static IPromise<List<Res.Leaflet>> QueryByIds(List<string> ids)
            {
                Req.QueryByIds req = new Req.QueryByIds { ids = ids };
                return post<Res.LeafletsResponse>("/leaflets/queryByIds", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Leaflets(res.leaflets);
                });
            }

            public static IPromise<List<Res.Signatory>> QuerySignatories(string leafletId)
            {
                return get<Res.SignatoriesResponse>("/leaflets/" + leafletId + "/signatories")
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Signatories(res.signatories);
                });
            }

            public static IPromise<Res.Leaflet> CreateLeaflet(
                uint placeId,
                uint protestTypeId,
                string bannerId,
                List<string> bannerWords)
            {
                Req.CreateLeaflet req = new Req.CreateLeaflet
                {
                    placeId = placeId,
                    protestTypeId = protestTypeId,
                    bannerId = bannerId,
                    bannerWords = bannerWords
                };
                return post<Res.LeafletResponse>("/leaflets", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Leaflet(res.leaflet);
                });
            }

            public static IPromise<Res.Signatory> SignLeaflet(
                string leafletId,
                string bannerId,
                List<string> bannerWords)
            {
                Req.SignLeaflet req = new Req.SignLeaflet
                {
                    bannerId = bannerId,
                    bannerWords = bannerWords,
                };
                return post<Res.SignatoryResponse>("/leaflets/" + leafletId + "/sign", req)
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Signatory(res.signatory);
                });
            }

            public static IPromise<Res.Leaflet> DestroyLeaflet(
                string leafletId)
            {
                return post<Res.LeafletResponse>("/leaflets/" + leafletId + "/destroy")
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Leaflet(res.leaflet);
                });
            }
        }
    }
}