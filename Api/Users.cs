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

            public static IPromise<bool> CheckNickname(string nickname)
            {
                RecordNicknameRequest req = new RecordNicknameRequest { nickname = nickname };
                return post<RecordSuccessResponse>("/users/checkNickname", req)
                .Then(res => res.success);
            }

            public static IPromise<bool> SetNickname(string nickname)
            {
                RecordNicknameRequest req = new RecordNicknameRequest { nickname = nickname };
                return put<RecordSuccessResponse>("/users/setNickname", req)
                .Then(res => res.success);
            }
        }
    }
}