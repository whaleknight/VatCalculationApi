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
        public IActionResult Calculate([FromBody] VatCalculationRequest request)
        {
            try
            {
                var response = _vatCalculatorService.Calculate(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
