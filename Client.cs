using Proyecto26;
using RSG;
using UnityEngine;
using System.Collections.Generic;

namespace ProtestGoClient
{

    public static partial class Client
    {
        // Base backend URL
        public static string BaseUrl = "https://pgo.panki.ru/";
        public static string UnityId;
        public static string AccessToken;
        public static bool Debug = false;

        private static RequestHelper buildRequest(string endpoint, object body = null)
        {
            string path = string.Join("/", endpoint).TrimEnd('/').TrimStart('/').Replace("//", "/");
            RequestHelper req = new RequestHelper
            {
                Uri = BaseUrl.TrimEnd('/') + '/' + path,
                Headers = new Dictionary<string, string> {
                        { "Authorization", "Bearer " + AccessToken }
                    },
                Body = body,
                EnableDebug = Debug
            };
            log("Making request to " + req.Uri, body);
            return req;
        }

        private static IPromise<T> get<T>(string endpoint, object body = null)
        {
            RequestHelper request = buildRequest(endpoint, body);
            return RestClient.Get<T>(request)
            .Catch(err => throw mapError(err));
        }

        private static IPromise<T> post<T>(string endpoint, object body)
        {
            RequestHelper request = buildRequest(endpoint, body);
            return RestClient.Post<T>(request)
            .Catch(err => throw mapError(err));
        }

        private static void log(string msg, object data = null)
        {
            if (!Debug) return;
            if (data != null)
            {
                string obj = JsonUtility.ToJson(data, true);
                msg = msg + ": " + obj;
            }
            UnityEngine.Debug.Log(msg);
        }

        private static System.Exception mapError(System.Exception e)
        {
            if (e is RequestException)
            {
                RequestException err = (RequestException)e;

                if (err.IsNetworkError) return new Errors.NetworkError(err.Message);

                ErrorResponse res = JsonUtility.FromJson<ErrorResponse>(err.Response);
                log("Error response", res);

                switch (err.StatusCode)
                {
                    case 400: return new Errors.InvalidArgumentsError(res.message != "" ? res.message : res.error);
                    case 401: return new Errors.UnauthorizedError();
                    case 404: return new Errors.NotFoundError();
                    case 500: return new Errors.ServerError();
                }
                return new Errors.UnknownError(err.Message);
            }
            return e;
        }
    }
}