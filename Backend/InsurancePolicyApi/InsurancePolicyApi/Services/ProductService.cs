using InsurancePolicyApi.DTOs.Product;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;

namespace InsurancePolicyApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            return await MapToResponseList(await _repository.GetAllAsync());
        }
        public async Task<IEnumerable<ProductResponse>> GetAllActiveAsync()
        {
            return await MapToResponseList(await _repository.GetActiveAsync());
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            return await MapToResponse(await _repository.GetByIdAsync(id));
        }

        public async Task<ProductResponse> AddAsync(ProductRequest request)
        {
            var exists = await _repository.ExistsByNameAsync(request.ProductName);

            if (exists)
                throw new InvalidOperationException("Product name already exists.");

            var product = new InsuranceProduct
            {
                ProductName = request.ProductName,
                ProductType = request.ProductType,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return await MapToResponse(await _repository.AddAsync(product));
        }

        public async Task<ProductResponse?> UpdateAsync(int id, ProductRequest product)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.ProductName = product.ProductName;
            existing.Description = product.Description;
            existing.ProductType = product.ProductType;
            existing.IsActive = product.IsActive;

            return await MapToResponse(await _repository.UpdateAsync(existing));
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            if (!product.IsActive)
                return true;

            return await _repository.DeactivateAsync(id);
        }

        public async Task<ProductResponse> MapToResponse(InsuranceProduct request)
        {
            var response = new ProductResponse()
            {
                ProductId = request.Id,
                ProductName = request.ProductName,
                ProductType = request.ProductType.ToString(),
                Description = request.Description,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate,
                IsActive = request.IsActive
            };
            return response;
        }
        public async Task<IEnumerable<ProductResponse>> MapToResponseList(IEnumerable<InsuranceProduct> requests)
        {
            var responses = new List<ProductResponse>();
            foreach(InsuranceProduct request in requests)
            {
                responses.Add(new ProductResponse()
                {
                    ProductId = request.Id,
                    ProductName = request.ProductName,
                    ProductType = request.ProductType.ToString(),
                    Description = request.Description,
                    CreatedDate = request.CreatedDate,
                    UpdatedDate = request.UpdatedDate,
                    IsActive = request.IsActive
                });
            }
            return responses;
        }
    }
}
