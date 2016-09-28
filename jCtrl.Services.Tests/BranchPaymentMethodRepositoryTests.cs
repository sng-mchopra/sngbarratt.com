using jCtrl.Services.Core.Repositories;
using NUnit.Framework;
using jCtrl.Services.Core.Domain.Advert;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;
using jCtrl.Services.Core.Domain.Branch;

namespace jCtrl.Services.Tests
{
    [TestFixture]
    public class BranchPaymentMethodRepositoryTests
    {
        private IList<BranchPaymentMethod> methods;
        private IBranchPaymentMethodRepository paymentMethodRepository;
        private int branchId;

        [SetUp]
        public void SetUp()
        {
            InitialiseTestData();

            Mock<IBranchPaymentMethodRepository> mockPaymentMethodRepository = new Mock<IBranchPaymentMethodRepository>();

            mockPaymentMethodRepository.Setup(mr => mr.GetPaymentMethodsByBranch(It.IsAny<int>())).ReturnsAsync(methods.Where(x => x.Branch_Id == branchId));

            paymentMethodRepository = mockPaymentMethodRepository.Object;
        }

        [Test]
        public void Get_PaymentMethodsByBranch()
        {
            IEnumerable<BranchPaymentMethod> methods = paymentMethodRepository.GetPaymentMethodsByBranch(branchId).Result;

            Assert.AreEqual(5, methods.Count());
        }


        public void InitialiseTestData()
        {
            branchId = 1;

            methods = new List<BranchPaymentMethod>
            {
                new BranchPaymentMethod { Id = 1, Branch_Id = 1, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 2, Branch_Id = 1, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 3, Branch_Id = 1, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 4, Branch_Id = 1, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 5, Branch_Id = 1, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 6, Branch_Id = 2, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 7, Branch_Id = 2, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 8, Branch_Id = 2, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 9, Branch_Id = 2, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 9, Branch_Id = 2, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 10, Branch_Id = 3, PaymentMethod_Code = "CC", SortOrder = 3, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 11, Branch_Id = 3, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 12, Branch_Id = 3, PaymentMethod_Code = "ID", SortOrder = 2, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 13, Branch_Id = 3, PaymentMethod_Code = "BT", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 14, Branch_Id = 3, PaymentMethod_Code = "PP", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 15, Branch_Id = 3, PaymentMethod_Code = "PC", SortOrder = 6, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 16, Branch_Id = 4, PaymentMethod_Code = "CC", SortOrder = 3, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 17, Branch_Id = 4, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 18, Branch_Id = 4, PaymentMethod_Code = "SF", SortOrder = 2, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 19, Branch_Id = 4, PaymentMethod_Code = "BT", SortOrder = 4, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 20, Branch_Id = 4, PaymentMethod_Code = "PP", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 21, Branch_Id = 4, PaymentMethod_Code = "PC", SortOrder = 6, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 22, Branch_Id = 5, PaymentMethod_Code = "CC", SortOrder = 2, IsDefault = true, IsActive = true },
                new BranchPaymentMethod { Id = 23, Branch_Id = 5, PaymentMethod_Code = "AC", SortOrder = 1, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 24, Branch_Id = 5, PaymentMethod_Code = "BT", SortOrder = 3, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 25, Branch_Id = 5, PaymentMethod_Code = "PC", SortOrder = 5, IsDefault = false, IsActive = true },
                new BranchPaymentMethod { Id = 26, Branch_Id = 5, PaymentMethod_Code = "PP", SortOrder = 4, IsDefault = false, IsActive = true }
            };
            
        }
    }
}
