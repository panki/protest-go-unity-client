using Proyecto26;
using RSG;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ProtestGoClient
{
    public static partial class Client
    {

        // Settings
        private static string baseUrl = "https://dev.protest-go.com/";
        private static bool debug = false;

        // App
        private static string appKey;
        private static string appSecret;

        // Geo
        private static float lat = 0;
        private static float lon = 0;

        // User
        private static string accessToken;
        private static string deviceId = SystemInfo.deviceUniqueIdentifier;

        public static void Init(string key, string secret)
        {
            appKey = key;
            appSecret = secret;
        }

        public static string SetAccessToken(string token)
        {
            accessToken = token;
            return accessToken;
        }

        public static void SetBaseUrl(string url)
        {
            baseUrl = url;
        }

        public static void SetDebug(bool enabled)
        {
            debug = enabled;
        }

        public static void SetLocation(float latitude, float longitude)
        {
            lat = latitude;
            lon = longitude;
        }

        private static string calcSign(object body)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            string data = JsonUtility.ToJson(body) + appSecret;
            byte[] input = Encoding.Default.GetBytes(data);
            byte[] result = md5.ComputeHash(input);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sBuilder.Append(result[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private static RequestHelper buildRequest(string endpoint, object body = null, Dictionary<string, string> args = null)
        {
            string path = string.Join("/", endpoint).TrimEnd('/').TrimStart('/').Replace("//", "/");
            string signature = calcSign(body);

            Dictionary<string, string> headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " + accessToken },
                { "X-Auth", appKey + ":" + signature },
                { "X-Geolocation", "Position=["+ lat + "," + lon + "]; Signature=" + calcSign(new { lon=lon, lat=lat })+";" }
            };

            RequestHelper req = new RequestHelper
            {
                Uri = baseUrl.TrimEnd('/') + '/' + path,
                Headers = headers,
                Params = args,
                Body = body,
                EnableDebug = debug
            };

            log("Request to: " + req.Uri, body);
            return req;
        }

        private static IPromise<T> get<T>(string endpoint, Dictionary<string, string> args = null)
        {
            RequestHelper request = buildRequest(endpoint, null, args);
            return RestClient.Get<T>(request)
            .Catch(err => throw mapError(err));
        }

        private static IPromise<T> post<T>(string endpoint, object body = null)
        {
            RequestHelper request = buildRequest(endpoint, body);
            return RestClient.Post<T>(request)
            .Catch(err => throw mapError(err));
        }

        private static IPromise<T> put<T>(string endpoint, object body = null)
        {
            RequestHelper request = buildRequest(endpoint, body);
            return RestClient.Put<T>(request)
            .Catch(err => throw mapError(err));
        }

        private static void log(string msg, object data = null)
        {
            if (!debug) return;
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

                if (err.IsNetworkError) return new Err.NetworkError(err.Message);

                Res.Error res = JsonUtility.FromJson<Res.Error>(err.Response);
                log("Error response", res);

                switch (err.StatusCode)
                {
                    case 400: return new Err.InvalidArgumentsError(res.message != "" ? res.message : res.error);
                    case 401: return new Err.UnauthorizedError();
                    case 404: return new Err.NotFoundError();
                    case 500: return new Err.ServerError();
                }
                return new Err.UnknownError(err.Message);
            }
            return e;
        }
    }
}