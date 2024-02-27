using VatCalculationApi.Model;
namespace VatCalculationApi.Service
{
    public interface IVatCalculatorService
    {
        VatCalculationResponse Calculate(VatCalculationRequest request);
    }
}
