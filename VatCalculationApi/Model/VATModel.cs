namespace VatCalculationApi.Model
{
    public class VatCalculationRequest
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? VatRate { get; set; }
    }

    public class VatCalculationResponse
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal VatAmount { get; set; }
    }

}
