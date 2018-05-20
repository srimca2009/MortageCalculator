using MortgageCalculator.Dto;
using System.Collections.Generic;

namespace MortgageCalculator.Service
{
    public interface IMortgageService
    {
        /// <summary>
        /// Get all mortages
        /// </summary>
        /// <returns></returns>
        IList<Mortgage> GetAllMortgages();

        /// <summary>
        /// Get mortage by id
        /// </summary>
        /// <param name="mortageId"></param>
        /// <returns></returns>
        Mortgage GetbyId(int mortageId);

        /// <summary>
        /// Mortgage Loan calculations
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="interest"></param>
        /// <param name="numberOfYears"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<LoanViewModel> LoanCalculation(double loanAmount, double interest, int numberOfYears, string type);
    }
}
