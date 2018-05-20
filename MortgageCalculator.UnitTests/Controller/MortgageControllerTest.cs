using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Dto;
using MortgageCalculator.Service;
using MortgageCalculator.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MortgageCalculator.UnitTests.Controller
{
    [TestClass]
    public class MortgageControllerTest
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
        public void Mortgages_Get_All()
        {
            //Arrange
            _mortgageServiceMock.Setup(x => x.GetAllMortgages()).Returns(_listMortgages);
            var result = ((_objController.Index() as ViewResult).Model) as List<Mortgage>;
            Assert.AreEqual(result.Count,2);
        }

        [TestMethod]
        public void Mortgages_Get_All_NotEqual()
        {
            //Arrange
            _mortgageServiceMock.Setup(x => x.GetAllMortgages()).Returns(_listMortgages);
            var result = ((_objController.Index() as ViewResult).Model) as List<Mortgage>;
            Assert.AreNotEqual(result.Count, 3);
        }


        [TestMethod]
        public void Mortgage_Get()
        {
            //Arrange
            _mortgageServiceMock.Setup(x => x.GetbyId(1)).Returns(_mortgage);
            var result = ((_objController.GetById(1) as ViewResult).Model) as Mortgage;
            Assert.AreEqual(result.MortgageId, 1);
        }

        [TestMethod]
        public void Mortgage_Get_NotEqual()
        {
            //Arrange
            _mortgageServiceMock.Setup(x => x.GetbyId(1)).Returns(_mortgage);
            var result = ((_objController.GetById(1) as ViewResult).Model) as Mortgage;
            Assert.AreNotEqual(result.MortgageId, 3);
        }
    }
}
