namespace InsurancePolicyApi.DTOs.Common
{
    /// <summary>Consistent envelope for every successful response (§11.10).</summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ApiResponse() { }

        public ApiResponse(T? data, string message = "")
        {
            Data = data;
            Message = message;
        }

        public static ApiResponse<T> Ok(T? data, string message = "") => new(data, message);
    }
}
