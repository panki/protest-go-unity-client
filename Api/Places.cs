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
        public class Place
        {
            public uint id;
            public string symId;
            public float lat;
            public float lon;
            public uint radius;
            public uint modeId;
            public List<uint> allowedProtestTypes;
            public string protestId;
            public string leafletId;

            [System.NonSerialized]
            public Protest protest;

            [System.NonSerialized]
            public Leaflet leaflet;

        }

        [Serializable]
        class PlacesResponse
        {
            public List<Place> places = null;
            public Graph graph = null;
        }

        [Serializable]
        class PlaceResponse
        {
            public Place place = null;
            public Graph graph = null;
        }
    }

    public static partial class Client
    {
        public static class Places
        {
            /*
            GetAll - request all places
            */
            public static IPromise<List<Res.Place>> QueryAll()
            {
                return get<Res.PlacesResponse>("/places")
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Places(res.places);
                });
            }

            public static IPromise<Res.Place> GetById(uint id)
            {
                return get<Res.PlaceResponse>("/places/" + id.ToString())
                .Then(res =>
                {
                    GraphMap g = new GraphMap(res.graph);
                    return g.Place(res.place);
                });
            }
        }
    }
}