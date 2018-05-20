using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Api.Controllers;
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
        MortgageController _objController;
        List<Mortgage> _listMortgages;
        Mortgage _mortgage;

        [TestInitialize]
        public void Initialize()
        {

            _mortgageServiceMock = new Mock<IMortgageService>();
            _objController = new MortgageController(_mortgageServiceMock.Object);


            _listMortgages = new List<Mortgage>() {
             new Mortgage() {
                 MortgageId =1, Name="Test",MortgageType=MortgageType.Fixed, InterestRepayment = InterestRepayment.InterestOnly, EffectiveStartDate= DateTime.Now.AddDays(-365),EffectiveEndDate=DateTime.Now, TermsInMonths=12, CancellationFee=0,EstablishmentFee=1000, InterestRate=13.5M },
                         new Mortgage() {
                 MortgageId =2, Name="TestOne",MortgageType=MortgageType.Variable, InterestRepayment = InterestRepayment.InterestOnly, EffectiveStartDate= DateTime.Now.AddDays(-365),EffectiveEndDate=DateTime.Now, TermsInMonths=12, CancellationFee=0,EstablishmentFee=1000, InterestRate=13.5M },
            };


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
            var dd = _mortgageServiceMock.Setup(x => x.GetAllMortgages()).Returns(_listMortgages);
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
    }
}
