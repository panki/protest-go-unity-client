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
                return get<RecordInitResponse>("/init");
            }

            /*
            Register - registers new anonymous user,
            returns access token for future requests.
            */
            public static IPromise<string> Register()
            {
                return post<RecordRegisterResponse>("/register", new RecordRegisterRequest { unityId = deviceId })
                .Then(res =>
                {
                    accessToken = res.token;
                    return accessToken;
                });
            }
        }


    }
}