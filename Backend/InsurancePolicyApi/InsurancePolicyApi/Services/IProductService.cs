using InsurancePolicyApi.DTOs.Product;
using InsurancePolicyApi.Entities;

namespace InsurancePolicyApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<IEnumerable<ProductResponse>> GetAllActiveAsync();

        Task<ProductResponse?> GetByIdAsync(int id);

        Task<ProductResponse> AddAsync(ProductRequest request);

        Task<ProductResponse?> UpdateAsync(int id, ProductRequest product);

        Task<bool> DeactivateAsync(int id);

        Task<ProductResponse> MapToResponse(InsuranceProduct request);
        Task<IEnumerable<ProductResponse>> MapToResponseList(IEnumerable<InsuranceProduct> requests);
    }
}
