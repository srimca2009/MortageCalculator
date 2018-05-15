using MortgageCalculator.Api.Models;
using MortgageCalculator.Api.Repos;
using MortgageCalculator.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageCalculator.Api.Services
{
    public class MortgageService : IMortgageService
    {
        private readonly IMortgageRepo _mortgageRepo;
        public MortgageService() : this(new MortgageRepo())
        { }

        public MortgageService(IMortgageRepo mortgageRepo)
        {
            this._mortgageRepo = mortgageRepo;
        }

        public IList<Mortgage> GetAllMortgages()
        {
            return _mortgageRepo.GetAll();
        }

        public Mortgage GetbyId(int mortageId)
        {
            return _mortgageRepo.GetAllMortgages().FirstOrDefault(x => x.MortgageId == mortageId);
        }

        public List<LoanViewModel> LoanCalculation(double loanAmount,double interest, int numberOfYears, MortgageType type)
        {
            List<LoanViewModel> result = new List<LoanViewModel>();

            // rate of interest and number of payments for monthly payments
            var rateOfInterest = interest / 1200;
            var numberOfPayments = numberOfYears * 12;

            var rateOfInterestPerMonth = rateOfInterest * 100;

            for(int i=0;i<= numberOfPayments; i++)
            {
                var principalAmount = loanAmount * rateOfInterest;

                // loan amount = (interest rate * loan amount) / (1 - (1 + interest rate)^(number of payments * -1))
                var paymentAmount = (rateOfInterest * loanAmount) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));

                var interestAmount = paymentAmount - principalAmount;

                if(type == MortgageType.Variable)
                {
                    loanAmount -= principalAmount;
                }

                var EMI = new LoanViewModel();

                EMI.EMIAmount       = paymentAmount;
                EMI.InterestAmount  = interestAmount;
                EMI.PrincipalAmount = principalAmount;

                result.Add(EMI);
            }
            return result;
        }
    }
}