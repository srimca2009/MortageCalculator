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
    }
}