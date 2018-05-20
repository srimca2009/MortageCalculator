using MortgageCalculator.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Repository
{
    public class MortgageRepository : IMortgageRepository
    {
        private const string CacheKey = "mortage";

        /// <summary>
        ///  Get All mortages
        /// </summary>
        /// <returns></returns>
        public IList<Mortgage> GetAll()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
                return (IList<Mortgage>)cache.Get(CacheKey);
            else
            {
                IList<Mortgage> getAllMortgages = this.GetAllMortgages();

                // Store data in the cache
                CacheItemPolicy cacheItemPolicy     = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration  = DateTime.Now.AddHours(24.00);
                cache.Add(CacheKey, getAllMortgages, cacheItemPolicy);

                return getAllMortgages;
            }
        }

        /// <summary>
        /// Get all mortages from cache
        /// </summary>
        /// <returns></returns>
        private List<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages.OrderBy(x=>x.MortgageType).ThenBy(x=>x.InterestRate).ToList();
                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgages)
                {
                    result.Add(new Mortgage()
                    {
                        Name                = mortgage.Name,
                        EffectiveStartDate  = mortgage.EffectiveStartDate,
                        EffectiveEndDate    = mortgage.EffectiveEndDate,
                        CancellationFee     = mortgage.CancellationFee,
                        EstablishmentFee    = mortgage.CancellationFee,
                        InterestRepayment   = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.InterestRepayment.ToString()),
                        MortgageId          = mortgage.MortgageId,
                        MortgageType        = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                        InterestRate        = mortgage.InterestRate
                    }
                    );
                }
                return result;
            }
        }
    }
}
