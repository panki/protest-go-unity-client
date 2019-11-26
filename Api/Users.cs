using RSG;

namespace ProtestGoClient
{

    public static partial class Client
    {
        public static class Users
        {
            /*
            Me - requests current user's information.
            */
            public static IPromise<RecordMeResponse> Me()
            {
                return get<RecordMeResponse>("/users/me");
            }
        }
    }
}