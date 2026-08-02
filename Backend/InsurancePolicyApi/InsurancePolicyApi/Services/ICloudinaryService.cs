namespace InsurancePolicyApi.Services
{
    public interface ICloudinaryService
    {
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
