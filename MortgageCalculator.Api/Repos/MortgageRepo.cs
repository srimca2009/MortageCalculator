using MortgageCalculator.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MortgageCalculator.Api.Repos
{
    public interface IMortgageRepo
    {
        List<Mortgage> GetAllMortgages();
        IList GetAll();
    }

    public class MortgageRepo : IMortgageRepo
    {
        private const string CacheKey = "mortage";
        public IList GetAll()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
                return (IList)cache.Get(CacheKey);
            else
            {
                IList getAllMortgages = this.GetAllMortgages();

                // Store data in the cache
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, getAllMortgages, cacheItemPolicy);

                return getAllMortgages;
            }
        }
       
        public List<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages.ToList();
                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgages)
                {
                    result.Add(new Mortgage()
                        {
                            Name = mortgage.Name,
                            EffectiveStartDate = mortgage.EffectiveStartDate,
                            EffectiveEndDate = mortgage.EffectiveEndDate,
                            CancellationFee = mortgage.CancellationFee,
                            EstablishmentFee = mortgage.CancellationFee,
                            InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.MortgageType.ToString()),
                            MortgageId = mortgage.MortgageId,
                            MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                           //TermsInMonths = mortgage.TermsInMonths
                        }
                    );
                }
                return result;
            }
        }
    }
}