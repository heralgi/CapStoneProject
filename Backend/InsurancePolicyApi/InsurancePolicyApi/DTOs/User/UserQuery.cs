using InsurancePolicyApi.DTOs.Common;
using InsurancePolicyApi.Entities.Enums;

namespace InsurancePolicyApi.DTOs.User
{
    /// <summary>Filters for the paginated user listing (§26.5): role, active status.</summary>
    public class UserQuery : PageQuery
    {
        public UserRole? Role { get; set; }

        public bool? IsActive { get; set; }
    }
}
