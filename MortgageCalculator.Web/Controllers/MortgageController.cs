using MortgageCalculator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MortgageCalculator.Web.Controllers
{
    public class MortgageController : Controller
    {
        private readonly IMortgageService _mortgageService;


        public MortgageController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }

        // GET: Mortgage
        public ActionResult Index()
        {
            return View(_mortgageService.GetAllMortgages());
        }

        public ActionResult GetById(int id)
        {
            return View(_mortgageService.GetbyId(id));
        }
    }
}