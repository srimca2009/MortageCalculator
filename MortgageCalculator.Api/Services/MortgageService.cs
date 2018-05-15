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

        public List<Mortgage> GetAllMortgages()
        {
            return _mortgageRepo.GetAllMortgages();
        }

        public Mortgage GetbyId(int mortageId)
        {
            return _mortgageRepo.GetAllMortgages().FirstOrDefault(x => x.MortgageId == mortageId);
        }

        public object FixedCalculation(double loanAmount,double interest, int numberOfYears)
        {
            //var loanAmount = 2000000;
            //var interest = 13.5;
            //var numberOfYears = 10;

            // rate of interest and number of payments for monthly payments
            var rateOfInterest = interest / 1200;
            var numberOfPayments = numberOfYears * 12;

            var rateOfInterestPerMonth = rateOfInterest * 100;

            //
            var principalAmount = loanAmount * rateOfInterest;

            // loan amount = (interest rate * loan amount) / (1 - (1 + interest rate)^(number of payments * -1))
            var paymentAmount = (rateOfInterest * loanAmount) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));

            return paymentAmount;
        }
    }
}