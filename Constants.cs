namespace ProtestGoClient.Constants
{
    // Any network connection error
    public static class ProtestMode
    {
        public static readonly uint Leaflet = 1;
        public static readonly uint Protest = 2;
    }

    public static class AvatarTypes
    {
        public static readonly uint Initial = 1;
        public static readonly uint OnSell = 2;
    }

    public static class Currency
    {
        public static readonly string Real = "real";
        public static readonly string Libero = "libero";
        public static readonly string Ordero = "ordero";
    }

    public static class Events
    {
        public static readonly string ProtestCreated = "protest.created";
        public static readonly string ProtestJoined = "protest.joined";
        public static readonly string ProtestLeaved = "protest.leaved";
        public static readonly string ProtestFinished = "protest.finished";
    }

}