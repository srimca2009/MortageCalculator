using MortgageCalculator.Dto;
using MortgageCalculator.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageCalculator.Service
{
    public class MortgageService : IMortgageService
    {
        private readonly IMortgageRepository _mortgageRepository;
        public MortgageService(IMortgageRepository mortgageRepository)
        {
            this._mortgageRepository = mortgageRepository;
        }

        public IList<Mortgage> GetAllMortgages()
        {
            return _mortgageRepository.GetAll();
        }

        public Mortgage GetbyId(int mortageId)
        {
            return _mortgageRepository.GetAll().FirstOrDefault(x => x.MortgageId == mortageId);
        }

        public List<LoanViewModel> LoanCalculation(double loanAmount, double interest, int numberOfYears, string type)
        {
            List<LoanViewModel> result = new List<LoanViewModel>();

            double emiAmount = 0, paymentAmount=0, principalAmount=0,balanceAmount=0;
            // rate of interest and number of payments for monthly payments
            var rateOfInterest = interest / 1200;
            var numberOfPayments = numberOfYears * 12;

            var rateOfInterestPerMonth = rateOfInterest * 100;

            for (int i = 0; i < numberOfPayments; i++)
            {
                var interestAmt = loanAmount * rateOfInterest;
                if (emiAmount == 0)
                {
                    paymentAmount = (rateOfInterest * loanAmount) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));

                    emiAmount = paymentAmount;
                }
                   
                principalAmount = emiAmount - interestAmt;

                if (type == MortgageType.Variable.ToString())
                {
                    loanAmount -= principalAmount;
                    balanceAmount = loanAmount;
                }
                else
                {
                    balanceAmount -= 0;
                }

                var EMI = new LoanViewModel();

                EMI.EMIAmount = Math.Round(emiAmount, 2);
                EMI.InterestAmount = Math.Round(interestAmt, 2);
                EMI.PrincipalAmount =Math.Round(principalAmount,2);
                EMI.EMIDate = DateTime.Now.AddMonths(i);
                EMI.TotalAmount = Math.Round((EMI.PrincipalAmount + EMI.InterestAmount),2);
                EMI.BalanceAmount = Math.Round(balanceAmount, 2);
                result.Add(EMI);
            }
            return result;
        }
    }
}
