namespace ProtestGoClient
{
    namespace Errors
    {
        // Any network connection error
        public class NetworkError : System.Exception
        {
            public NetworkError(string message) : base(message) { }
        }

        // Internal server error (500)
        public class ServerError : System.Exception { }

        // Object not founf (404)
        public class NotFoundError : System.Exception { }

        public class UnauthorizedError : System.Exception { }

        // Not mapped error
        public class UnknownError : System.Exception
        {
            public UnknownError(string message) : base(message) { }
        }

        // Invalid arguments has been sent
        public class InvalidArgumentsError : System.Exception
        {
            public InvalidArgumentsError() : base() { }
            public InvalidArgumentsError(string message) : base(message) { }
        }
    }
}

