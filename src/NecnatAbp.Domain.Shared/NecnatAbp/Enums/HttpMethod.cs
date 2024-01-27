namespace NecnatAbp.Enums
{
    public enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Delete = 4,
        Connect = 5,
        Head = 6,
        Options = 7,
        Trace = 8
    }

    public static class HttpMethodUtil
    {
        public static HttpMethod? GetByName(string name)
        {
            switch (name.ToUpper())
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                case "HEAD":
                    return HttpMethod.Head;
                case "OPTIONS":
                    return HttpMethod.Options;
                case "TRACE":
                    return HttpMethod.Trace;
                default:
                    return null;
            }
        }
    }
}
