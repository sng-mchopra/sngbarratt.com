using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class AdvertRepositoryTests
    {
        private IList<Advert> adverts;
        private IAdvertRepository advertRepository;
        private Guid advertToFind;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IAdvertRepository> mockAdvertRepository = new Mock<IAdvertRepository>();

            mockAdvertRepository.Setup(r => r.GetAll()).ReturnsAsync(adverts);

            mockAdvertRepository.Setup(mr => mr.GetAdvert(It.IsAny<Guid>())).ReturnsAsync(adverts.Where(x => x.Id == advertToFind).SingleOrDefault());

            advertRepository = mockAdvertRepository.Object;
        }

        [Test]
        public void Get_Adverts()
        {
            IEnumerable<Advert> adverts = advertRepository.GetAll().Result;

            Assert.AreEqual(4, adverts.Count());
            Assert.IsNotNull(adverts);
        }

        [Test]
        public void Get_AdvertById()
        {
            Advert advert = advertRepository.GetAdvert(advertToFind).Result;

            Assert.IsNotNull(advert);
            Assert.IsInstanceOf(typeof(Advert), advert);
            Assert.AreEqual("Advert Four", advert.Title);
        }

        public void InitialiseTestData()
        {
            advertToFind = Guid.Parse("b36aaaa2-94bc-48fc-a36c-1cf377a9a35b");

            adverts = new List<Advert>()
            {
                new Advert
                {
                    Id = Guid.Parse("a87f6f18-6c67-4b92-9b91-7d6c7a2d8b66"),
                    AdvertType_Id = "I",
                    Title = "E-Type Hillclimb Day",
                    Description = "Take to the track in our E-Type.<br/> Sunday 31st May - Shelsley Walsh.<br/> Limited spaces.  Book your place now.",
                    ImageFilename_Desktop = "bgfour.jpg",
                    ImageFilename_Device = "bgfour_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = true,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("6330da08-73be-4542-afdc-c8ec02699208"),
                    AdvertType_Id = "I",
                    Title = "Advert Two",
                    Description = "Advert two has a link to an event",
                    LinkUrl = "~/uk/events/b2c82716-2106-4f38-b546-713be3671e23",
                    ImageFilename_Desktop = "bgten.jpg",
                    ImageFilename_Device = "bgten_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("d7eab64e-85f8-4a70-b4cb-8f034d2b8d3f"),
                    AdvertType_Id = "I",
                    Title = "Advert Three",
                    Description = "Advert three has a link to a product offer",
                    LinkUrl = "~/uk/offers/44cb9ad7-b604-48d4-995f-ba786b094c29",
                    ImageFilename_Desktop = "bgseven.jpg",
                    ImageFilename_Device = "bgseven_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Advert
                {
                    Id = Guid.Parse("b36aaaa2-94bc-48fc-a36c-1cf377a9a35b"),
                    AdvertType_Id = "I",
                    Title = "Advert Four",
                    Description = "Advert four has a link to a product",
                    LinkUrl = "~/uk/products/91bf8fc7-2c80-4e9c-8ade-6368dc45314e",
                    ImageFilename_Desktop = "bgfive.jpg",
                    ImageFilename_Device = "bgfive_DEV.jpg",
                    ExpiresUtc = DateTime.Today.AddDays(30),
                    IsPriority = false,
                    IsActive = true,
                    Branch_Id = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }
            };
        }
    }
}
