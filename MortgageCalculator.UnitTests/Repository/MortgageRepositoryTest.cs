using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Repository;
using System.Linq;

namespace MortgageCalculator.UnitTests
{
    [TestClass]
    public class MortgageRepositoryTest
    {
        MortgageData.MortgageDataContext _databaseContext;
        MortgageRepository _objRepo;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext = new MortgageData.MortgageDataContext();
            _objRepo = new MortgageRepository();

        }

        [TestMethod]
        public void Mortgage_Repository_Get_ALL()
        {
            var result = _objRepo.GetAll().ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.Count);
        }


        [TestMethod]
        public void Mortgage_Repository_Get_ALL_NotEqual()
        {
            //Act
            var result = _objRepo.GetAll().ToList();
            Assert.AreNotEqual(9, result.Count);
        }
    }
}
