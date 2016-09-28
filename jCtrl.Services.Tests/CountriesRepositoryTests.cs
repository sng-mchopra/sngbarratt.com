using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class CountriesRepositoryTests
    {
        private IList<Country> countries;
        private ICountriesRepository countryRepository;
        string countryCode;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<ICountriesRepository> mockCountryRepository = new Mock<ICountriesRepository>();

            mockCountryRepository.Setup(mr => mr.GetDeliveryCountryByCode(It.IsAny<string>())).ReturnsAsync(countries.Where(x => x.Code == countryCode).SingleOrDefault());

            countryRepository = mockCountryRepository.Object;
        }

        [Test]
        public void Get_DeliveryCountryByCode()
        {
            var deliveryCountry = countryRepository.GetDeliveryCountryByCode(countryCode).Result;

            Assert.AreEqual("NL", deliveryCountry.Code);
        }

        public void InitialiseTestData()
        {
            countryCode = "NL";

            countries = new List<Country>
            {
                new Country
                {
                    Code = "GB",
                    // Name = "United Kingdom",
                    InternationalDialingCode = "+44",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "FR",
                    // Name = "France",
                    InternationalDialingCode = "+33",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "DE",
                    // Name = "Germany",
                    InternationalDialingCode = "+49",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "NL",
                    // Name = "Netherlands",
                    InternationalDialingCode = "+31",
                    IsMemberOfEEC = true,
                    IsEuropean = true
                },
                new Country
                {
                    Code = "US",
                    // Name = "United States",
                    InternationalDialingCode = "+1",
                    IsMemberOfEEC = false,
                    IsEuropean = false
                }
            };
        }
    }
}
