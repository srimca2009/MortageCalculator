using MortgageCalculator.Dto;
using MortgageCalculator.Service;
using System.Collections.Generic;
using System.Web.Http;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {
        private readonly IMortgageService _mortgageService;
        public MortgageController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }
        // GET: api/Mortgage
        public IEnumerable<Mortgage> Get()
        {
            return _mortgageService.GetAllMortgages();
        }

        // GET: api/Mortgage/5
        public Mortgage Get(int id)
        {
            return _mortgageService.GetbyId(id);
        }
    }
}
