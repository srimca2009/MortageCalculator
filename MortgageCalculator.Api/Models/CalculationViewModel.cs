namespace MortgageCalculator.Api.Models
{
    public class CalculationViewModel
    {
        public double LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public int Years { get; set; }
        public string LoanType { get; set; }
    }
}