namespace InsurancePolicyApi.DTOs.Common
{
    /// <summary>Consistent pagination envelope for every list API (§11.10, §26.6).</summary>
    public class PagedResponse<T>
    {
        /// <summary>The list of returned records for the current page.</summary>
        public IReadOnlyList<T> Records { get; set; } = new List<T>();

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public long TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public bool IsLastPage { get; set; }

        /// <summary>Field used for sorting, where applicable.</summary>
        public string? SortField { get; set; }

        /// <summary>Ascending or descending, where applicable.</summary>
        public string? SortDirection { get; set; }

        public PagedResponse() { }

        public PagedResponse(
            IReadOnlyList<T> records,
            int currentPage,
            int pageSize,
            long totalRecords,
            string? sortField = null,
            string? sortDirection = null)
        {
            Records = records;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = pageSize > 0 ? (int)Math.Ceiling(totalRecords / (double)pageSize) : 0;
            IsLastPage = currentPage >= TotalPages;
            SortField = sortField;
            SortDirection = sortDirection;
        }
    }
}
