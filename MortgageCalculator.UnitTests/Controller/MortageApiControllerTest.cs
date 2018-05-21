using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Api.Controllers;
using MortgageCalculator.Api.Models;
using MortgageCalculator.Dto;
using MortgageCalculator.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageCalculator.UnitTests.Controller
{
    [TestClass]
    public class MortageApiControllerTest
    {
        private Mock<IMortgageService> _mortgageServiceMock;
        MortgageApiController _objController;
        List<Mortgage> _listMortgages;
        List<LoanViewModel> _listLoan;
        Mortgage _mortgage;
        CalculationViewModel calc;

        [TestInitialize]
        public void Initialize()
        {

            _mortgageServiceMock = new Mock<IMortgageService>();
            _objController = new MortgageApiController(_mortgageServiceMock.Object);


            _listMortgages = new List<Mortgage>() {
             new Mortgage() {
                 MortgageId =1, Name="Test",MortgageType=MortgageType.Fixed, InterestRepayment = InterestRepayment.InterestOnly, EffectiveStartDate= DateTime.Now.AddDays(-365),EffectiveEndDate=DateTime.Now, TermsInMonths=12, CancellationFee=0,EstablishmentFee=1000, InterestRate=13.5M },
                         new Mortgage() {
                 MortgageId =2, Name="TestOne",MortgageType=MortgageType.Variable, InterestRepayment = InterestRepayment.InterestOnly, EffectiveStartDate= DateTime.Now.AddDays(-365),EffectiveEndDate=DateTime.Now, TermsInMonths=12, CancellationFee=0,EstablishmentFee=1000, InterestRate=13.5M },
            };

            _listLoan = new List<LoanViewModel>()
            {
                  new LoanViewModel() { PrincipalAmount=200000, EMIAmount=35000, EMIDate=DateTime.Now.AddDays(-30), InterestAmount=13000, TotalAmount =35000 },
                  new LoanViewModel() { PrincipalAmount=200000, EMIAmount=33000, EMIDate=DateTime.Now, InterestAmount=13000, TotalAmount =35000 }
            };

            calc = new CalculationViewModel() { LoanAmount=200000, InterestRate=13.5, LoanType="Variable", Years=10 };


            _mortgage = new Mortgage()
            {
                MortgageId = 1,
                Name = "Test",
                MortgageType = MortgageType.Fixed,
                InterestRepayment = InterestRepayment.InterestOnly,
                EffectiveStartDate = DateTime.Now.AddDays(-365),
                EffectiveEndDate = DateTime.Now,
                TermsInMonths = 12,
                CancellationFee = 0,
                EstablishmentFee = 1000,
                InterestRate = 13.5M
            };

        }

        [TestMethod]
        public void Mortgage_Get_All()
        {
            _mortgageServiceMock.Setup(x => x.GetAllMortgages()).Returns(_listMortgages);
            var result = _objController.GetAll();
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod]
        public void Mortgage_Get_All_NotEqual()
        {
            _mortgageServiceMock.Setup(x => x.GetAllMortgages()).Returns(_listMortgages);
            var result = _objController.GetAll();
            Assert.AreNotEqual(result.Count(), 3);
        }

        [TestMethod]
        public void Mortgage_Get()
        {
            _mortgageServiceMock.Setup(x => x.GetbyId(1)).Returns(_mortgage);
            var result = _objController.Get(1);
            Assert.AreEqual(result.MortgageId, 1);
        }

        [TestMethod]
        public void Mortgage_Get_NotEqual()
        {
            _mortgageServiceMock.Setup(x => x.GetbyId(1)).Returns(_mortgage);
            var result = _objController.Get(1);
            Assert.AreNotEqual(result.MortgageId, 5);
        }

        [TestMethod]
        public void Mortgage_Get_LoanCalc()
        {
            _mortgageServiceMock.Setup(x => x.LoanCalculation(200000,5,10,"Variable")).Returns(_listLoan);
            var result = _objController.Calculation(calc);
            Assert.AreEqual(result.Count(), null);
        }

        [TestMethod]
        public void Mortgage_Get_LoanCalc_NotEqual()
        {
            _mortgageServiceMock.Setup(x => x.LoanCalculation(200000, 5, 10, "Variable")).Returns(_listLoan);
            var result = _objController.Calculation(calc);
            Assert.AreNotEqual(result.Count(), 3);
        }
    }
}
