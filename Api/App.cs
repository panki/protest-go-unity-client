using RSG;

namespace ProtestGoClient
{

    public static partial class Client
    {
        public static class App
        {
            /*
            Init - requests init configuration from server
            */
            public static IPromise<RecordInitResponse> Init()
            {
                return get<RecordInitResponse>("/init", new RecordInitRequest { });
            }

            /*
            Register - registers new anonymous user,
            returns access token for future requests.
            */
            public static IPromise<string> Register()
            {
                return post<RecordRegisterResponse>("/register", new RecordRegisterRequest { unityId = UnityId })
                .Then(res =>
                {
                    AccessToken = res.token;
                    return AccessToken;
                });
            }
        }


    }
}