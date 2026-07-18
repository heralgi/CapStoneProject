using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyApi.DTOs.Common
{
    /// <summary>
    /// Base pagination, sorting, and filtering input for list APIs (§26).
    /// Module-specific query DTOs derive from this and add their own filter fields.
    /// Defaults: page 1, size 10, max 100, sort by CreatedDate descending (§26.3).
    /// </summary>
    public class PageQuery
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        /// <summary>1-based page number; negatives rejected (PAG-RUL-007).</summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than zero.")]
        public int PageNumber { get; set; } = 1;

        /// <summary>Records per page; must be &gt; 0 and ≤ max (PAG-RUL-008/009).</summary>
        [Range(1, MaxPageSize, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = DefaultPageSize;

        /// <summary>Field to sort by; validated against an approved allow-list per module (SRT-RUL-003).</summary>
        public string? SortBy { get; set; }

        /// <summary>"asc" or "desc"; defaults to descending (SRT-RUL-002/005).</summary>
        public string? SortDirection { get; set; } = "desc";
    }
}
