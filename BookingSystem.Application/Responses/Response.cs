namespace BookingSystem.Application.Responses
{
    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
            Succeeded = true;
            Message = string.Empty;            
        }

        public Response(T data, bool succeeded, string message)
        {
            Data = data;
            Succeeded = succeeded;
            Message = message;            
        }

        public bool Succeeded { get; }
        public string Message { get; } = string.Empty;
        public T Data { get; set; }
    }
}
