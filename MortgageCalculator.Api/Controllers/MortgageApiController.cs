using MortgageCalculator.Dto;
using MortgageCalculator.Service;
using System.Collections.Generic;
using System.Web.Http;

namespace MortgageCalculator.Api.Controllers
{
    [RoutePrefix("api/Mortgage")]
    public class MortgageApiController : ApiController
    {
        private readonly IMortgageService _mortgageService;
        public MortgageApiController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }
        [HttpGet]
        [Route("GetAll")]
        // GET: api/Mortgage
        public IEnumerable<Mortgage> GetAll()
        {
            return _mortgageService.GetAllMortgages();
        }
        [HttpGet]
        [Route("GetById")]
        // GET: api/Mortgage/5
        public Mortgage Get(int id)
        {
            return _mortgageService.GetbyId(id);
        }
    }
}
