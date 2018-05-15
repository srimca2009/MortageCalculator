using System;

namespace MortgageCalculator.Api.Models
{
    public class LoanViewModel
    {
        public DateTimeOffset EMIDate { get; set; }
        public double EMIAmount { get; set; }
        public double InterestAmount { get; set; }
        public double PrincipalAmount { get; set; }
        public double TotalAmount { get; set; }
    }
}