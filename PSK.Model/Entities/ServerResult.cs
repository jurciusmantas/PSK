namespace PSK.Model.Entities
{
    public class ServerResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ServerResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
