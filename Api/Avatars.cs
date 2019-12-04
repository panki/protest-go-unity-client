using RSG;
using System.Collections.Generic;

namespace ProtestGoClient
{
    public static partial class Client
    {
        public static class Avatars
        {
            /*
            GetAll - request all avatars on sale
            */
            public static IPromise<List<RecordAvatarResponse>> GetAllOnSale()
            {
                return get<RecordAvatarsResponse>("/avatars").Then(res => res.avatars);
            }
        }
    }
}