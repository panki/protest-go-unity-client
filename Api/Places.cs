using RSG;
using System.Collections.Generic;

namespace ProtestGoClient
{
    public static partial class Client
    {
        public static class Places
        {
            /*
            GetAll - request all places
            */
            public static IPromise<List<RecordPlaceResponse>> GetAll()
            {
                return get<RecordPlacesResponse>("/places").Then(res => res.places);
            }
        }
    }
}