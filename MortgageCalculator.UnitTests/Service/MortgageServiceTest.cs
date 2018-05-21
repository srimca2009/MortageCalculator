using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Dto;
using MortgageCalculator.Repository;
using MortgageCalculator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.UnitTests
{
    [TestClass]
    public class MortgageServiceTest
    {

        private Mock<IMortgageRepository> _mockRepository;
        private IMortgageService _service;
        List<Mortgage> _listMortgages;
        Mortgage _mortgage;


        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IMortgageRepository>();
            _service = new MortgageService(_mockRepository.Object);

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
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            List<Mortgage> results = _service.GetAllMortgages() as List<Mortgage>;
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Mortgage_Get_All_NotEqual()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            List<Mortgage> results = _service.GetAllMortgages() as List<Mortgage>;
            Assert.AreNotEqual(4, results.Count);
        }


        [TestMethod]
        public void Mortgage_Get()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            Mortgage results = _service.GetbyId(1) as Mortgage;
            Assert.AreEqual(1, results.MortgageId);
        }

        [TestMethod]
        public void Mortgage_Get_NotEqual()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            Mortgage results = _service.GetbyId(1) as Mortgage;
            Assert.AreNotEqual(2, results.MortgageId);
        }

        [TestMethod]
        public void Mortgage_Get_Calc()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            List<LoanViewModel> results = _service.LoanCalculation(200000,13.5,10,"Variable") as List<LoanViewModel>;
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Mortgage_Get_Calc_NotEqual()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(_listMortgages);
            List<LoanViewModel> results = _service.LoanCalculation(200000, 13.5, 10, "Variable") as List<LoanViewModel>;
            Assert.IsNotNull(results);
            Assert.AreNotEqual(2, results.Count);
        }

    }
}
