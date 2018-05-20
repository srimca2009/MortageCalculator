using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Dto
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
