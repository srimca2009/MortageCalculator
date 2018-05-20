using MortgageCalculator.Dto;
using System.Collections.Generic;

namespace MortgageCalculator.Repository
{
    public interface IMortgageRepository
    {
        IList<Mortgage> GetAll();
    }
}
