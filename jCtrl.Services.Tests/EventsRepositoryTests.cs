using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain.Branch;
using jCtrl.Services.Core.Domain.Customer;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Order;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class EventsRepositoryTests
    {
        private IList<ShowEvent> events;
        private IList<Branch> branches;
        private IEventRepository eventRepository;
        private DateTime start;
        private DateTime end;
        private Guid eventId;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IEventRepository> mockEventRepository = new Mock<IEventRepository>();

            IQueryable<ShowEvent> eventsByDateRange = events
                .Where(e => e.EventDateTimes.OrderBy(t => t.StartsUtc).FirstOrDefault().StartsUtc <= end)
                .Where(e => e.EventDateTimes.OrderByDescending(t => t.EndsUtc).FirstOrDefault().EndsUtc > start)
                .AsQueryable();

            mockEventRepository.Setup(b => b.GetEventsByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(eventsByDateRange);

            IQueryable<ShowEvent> upcomingEvents = events
                .Where(e => e.EventDateTimes.OrderByDescending(t => t.EndsUtc).FirstOrDefault().EndsUtc > DateTime.Today)
                .AsQueryable();

            mockEventRepository.Setup(b => b.GetUpcomingEvents()).Returns(upcomingEvents);

            Func<Guid, ShowEvent> evt = (Guid evtId) => events.Where(e => e.Id == evtId).Single();

            mockEventRepository.Setup(b => b.GetEvent(It.IsAny<Guid>())).ReturnsAsync(evt(eventId));

            eventRepository = mockEventRepository.Object;
        }

        [Test]
        public void Get_EventsByDateRange()
        {
            var events = eventRepository.GetEventsByDateRange(start, end);

            Assert.AreEqual(1, events.Count());
        }

        [Test]
        public void Get_UpcomingEvents()
        {
            var events = eventRepository.GetUpcomingEvents();

            Assert.AreEqual(0, events.Count());
        }

        [Test]
        public void Get_Event()
        {
            var evt = eventRepository.GetEvent(eventId).Result;

            Assert.IsNotNull(evt);
            Assert.AreEqual(Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"), evt.Id);
        }

        public void InitialiseTestData()
        {
            start = DateTime.Now.AddYears(-2);
            end = DateTime.Parse("2015-06-06 20:00");
            eventId = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2");

                branches = new List<Branch>(){
                new Branch
                {
                    Id = 1,
                    SiteCode = "UK",
                    BranchCode = "SNG",
                    Name = "SNG Barratt UK",
                    AddressLine1 = "The Heritage Building",
                    AddressLine2 = "Stourbridge Road",
                    TownCity = "Bridgnorth",
                    CountyState = "Shropshire",
                    PostalCode = "WV15 6AP",
                    CountryName = "United Kingdom",
                    Country_Code = "GB",
                    PhoneNumber = "+44 (0)1746 765 432",
                    EmailAddress = "sales.uk@sngbarratt.com",
                    Currency_Code = "GBP",
                    Language_Id = 1,
                    FlagFilename = "../img/flags/GB.png",
                    Latitude = 52.528213m,
                    Longitude = -2.403453m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "UK Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 1,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 2,
                    SiteCode = "US",
                    BranchCode = "BAU",
                    Name = "SNG Barratt USA",
                    AddressLine1 = "92 Londonderry Turnpike",
                    AddressLine2 = "",
                    TownCity = "Manchester",
                    CountyState = "New Hampshire",
                    PostalCode = "03104",
                    CountryName = "United States",
                    Country_Code = "US",
                    PhoneNumber = "+1 800 452 4787",
                    EmailAddress = "sales.usa@sngbarratt.com",
                    Currency_Code = "USD",
                    Language_Id = 1,
                    FlagFilename = "../img/flags/US.png",
                    Latitude = 43.004570m,
                    Longitude = -71.393640m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "US Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                },
                new Branch
                {
                    Id = 3,
                    SiteCode = "NL",
                    BranchCode = "SNH",
                    Name = "SNG Barratt NL",
                    AddressLine1 = "Laarakkerweg 12",
                    AddressLine2 = "",
                    TownCity = "Oisterwijk",
                    CountyState = "",
                    PostalCode = "5061 JR",
                    CountryName = "Netherlands",
                    Country_Code = "NL",
                    PhoneNumber = "+31 13-5211552",
                    EmailAddress = "sales.nl@sngbarratt.com",
                    Currency_Code = "EUR",
                    Language_Id = 4,
                    FlagFilename = "../img/flags/NL.png",
                    Latitude = 51.585647m,
                    Longitude = 5.204178m,
                    Introductions = new List<BranchIntroduction> {
                        new BranchIntroduction { Language_Id = 1, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 2, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN FRENCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 3, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN GERMAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 4, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN DUTCH", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" },
                        new BranchIntroduction { Language_Id = 5, Intro = "NL Branch Intro Text. Established date, company registration number, vat number etc IN AMERICAN", RowVersion = 1, CreatedTimestampUtc = DateTime.UtcNow, CreatedByUsername = "Seed", UpdatedTimestampUtc = DateTime.UtcNow, UpdatedByUsername = "Seed" }
                    },
                    SortOrder = 4,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed"
                }

            };


            events = new List<ShowEvent>
            {
                new ShowEvent
                {
                    Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"),
                    Title = "Interclassics and Topmobiel",
                    Description = "Come and marvel at the finest and most exclusive vehicles in the Benelux, as well as everything else related to car parts and automobilia.  SNG Barratt Holland will be at the show, and taking Pre-Orders for collection.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"), StartsUtc = DateTime.Parse("2015-06-06 08:00"), EndsUtc = DateTime.Parse("2015-06-06 20:00") },
                        new ShowEventDateTime {Event_Id = Guid.Parse("9825fa0f-91ee-41bb-bf6d-1b7082eb9ef2"), StartsUtc = DateTime.Parse("2015-06-07 08:00"), EndsUtc = DateTime.Parse("2015-06-07 20:00") }
                    },
                    EventUrl = "http://www.ic-tm.nl/uk/",
                    Location = "MECC Maastrict, Netherlands",
                    MapUrl = "https://www.google.co.uk/maps/place/MECC+Maastricht/@50.8383184,5.7114246,15z/data=!4m2!3m1!1s0x47c0e984c10bc78d:0x11310fc6c0fade4c?hl=en",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 3,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branches.Single(b => b.Id == 3)
                },
                new ShowEvent
                {
                    Id = Guid.Parse("80a79f21-7530-47e8-b768-800d1a19a3c6"),
                    Title = "Event DE",
                    Description = "Quo nisi malis irure deserunt ea ex anim iudicem, nisi laborum si cupidatat, eiusmod dolore aliquip eiusmod. E summis arbitror sempiternum, laborum culpa lorem laboris esse. Multos quo eiusmod, aliquip cillum cillum ad nulla. Summis quibusdam ingeniis, labore ingeniis firmissimum qui quid probant e velit velit ut an quorum cohaerescant, do excepteur firmissimum si irure iis ne veniam excepteur, iis fugiat esse irure admodum, te litteris domesticarum. Qui ab veniam sunt elit.",
                    EventDateTimes = new List<ShowEventDateTime>
                    {
                        new ShowEventDateTime {Event_Id = Guid.Parse("80a79f21-7530-47e8-b768-800d1a19a3c6"), StartsUtc = DateTime.Parse("2015-08-18 08:00"), EndsUtc = DateTime.Parse("2015-08-18 20:00") }
                    },
                    Location = "Wolfsburg, Germany",
                    ImageFilename = "interclassics.jpg",
                    IsAttending = true,
                    IsActive = true,
                    Branch_Id = 2,
                    RowVersion = 1,
                    CreatedTimestampUtc = DateTime.UtcNow,
                    CreatedByUsername = "Seed",
                    UpdatedTimestampUtc = DateTime.UtcNow,
                    UpdatedByUsername = "Seed",
                    Branch = branches.Single(b => b.Id == 2)
                }
            };

            
            
        }
    }
}
