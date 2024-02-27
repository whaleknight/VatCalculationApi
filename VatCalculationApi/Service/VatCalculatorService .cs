using VatCalculationApi.Model;
namespace VatCalculationApi.Service
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculationResponse Calculate(VatCalculationRequest request)
        {
            if (request.VatRate is null || request.VatRate <= 0 || request.VatRate > 100)
                throw new ArgumentException("Invalid VAT rate.");

            decimal net, gross, vatAmount, vatRate = (decimal)request.VatRate / 100;

            if (request.Net.HasValue && request.Net > 0)
            {
                net = request.Net.Value;
                vatAmount = net * vatRate;
                gross = net + vatAmount;
            }
            else if (request.Gross.HasValue && request.Gross > 0)
            {
                gross = request.Gross.Value;
                net = gross / (1 + vatRate);
                vatAmount = gross - net;
            }
            else
            {
                throw new ArgumentException("Invalid amount input.");
            }

            return new VatCalculationResponse { Net = net, Gross = gross, VatAmount = vatAmount };
        }
    }
}
