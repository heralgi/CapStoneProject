namespace InsurancePolicyApi.DTOs.Common
{
    /// <summary>Consistent structure for every error response (§11.10, §18.2).</summary>
    public class ErrorResponse
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>HTTP response status code.</summary>
        public int StatusCode { get; set; }

        /// <summary>Error category (e.g. NotFound, Conflict, ValidationError).</summary>
        public string ErrorType { get; set; } = string.Empty;

        /// <summary>Clear, user-readable message.</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>API path where the error occurred.</summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>Optional field-level validation details.</summary>
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}
