using Microsoft.AspNetCore.Mvc;
using VatCalculationApi.Model;
using VatCalculationApi.Service;

namespace VatCalculationApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IVatCalculatorService _vatCalculatorService;

        public VatCalculatorController(IVatCalculatorService vatCalculatorService)
        {
            _vatCalculatorService = vatCalculatorService;
        }

        [HttpPost("Calculate")]
        public IActionResult Calculate(VatCalculationRequest request)
        {
            // Check for non-numeric or missing VAT rate
            if (request.VatRate <= 0 || request.VatRate is null)
            {
                return BadRequest("Missing or invalid VAT rate input.");
            }

            // Count how many of Net, Gross, or VATAmount values are provided
            int inputsProvided = new[] { request.Net, request.Gross, request.VatAmount }.Count(v => v.HasValue);

            // Validate exactly one value is provided
            if (inputsProvided != 1)
            {
                return BadRequest("Please provide exactly one value: either Net, Gross, or VAT Amount.");
            }

            // Validation for non-numeric or zero amount input
            if ((request.Net.HasValue && request.Net <= 0) || (request.Gross.HasValue && request.Gross <= 0) || (request.VatAmount.HasValue && request.VatAmount <= 0))
            {
                return BadRequest("Amount input must be a positive number.");
            }

            try
            {
                var response = _vatCalculatorService.Calculate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
