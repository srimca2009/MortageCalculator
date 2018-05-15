using MortgageCalculator.Api.Models;
using MortgageCalculator.Dto;
using System.Collections.Generic;

namespace MortgageCalculator.Api.Services
{
    public interface IMortgageService
    {
        /// <summary>
        /// Get all mortages
        /// </summary>
        /// <returns></returns>
        List<Mortgage> GetAllMortgages();

        /// <summary>
        /// Get mortage by id
        /// </summary>
        /// <param name="mortageId"></param>
        /// <returns></returns>
        Mortgage GetbyId(int mortageId);

        /// <summary>
        /// Loan Calculation
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="interest"></param>
        /// <param name="numberOfYears"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<LoanViewModel> LoanCalculation(double loanAmount, double interest, int numberOfYears, MortgageType type);
    }
}