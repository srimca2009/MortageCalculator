using MortgageCalculator.Api.Models;
using MortgageCalculator.Dto;
using MortgageCalculator.Service;
using System;
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

        [HttpPost]
        [Route("Calculation")]
        public List<LoanViewModel> Calculation(CalculationViewModel loan)
        {
            if (loan == null)
            {
                return null;
            }
            return _mortgageService.LoanCalculation(loan.LoanAmount, loan.InterestRate, loan.Years, loan.LoanType);
        }
    }
}
