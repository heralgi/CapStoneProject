using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimDocumentsController : ControllerBase
    {
        private readonly IClaimDocumentService _service;

        public ClaimDocumentsController(IClaimDocumentService service)
        {
            _service = service;
        }

        // GET: api/claimdocuments/claim/5
        [HttpGet("claim/{claimId:int}")]
        public async Task<IActionResult> GetDocumentsByClaim(int claimId)
        {
            var documents = await _service.GetDocumentsByClaimAsync(claimId);

            return Ok(documents);
        }

        // POST: api/claimdocuments
        [HttpPost]
        public async Task<IActionResult> AddDocument(ClaimDocument document)
        {
            var result = await _service.AddDocumentAsync(document);

            return Ok(result);
        }
    }
}
