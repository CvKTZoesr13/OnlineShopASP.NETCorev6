using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineShop.Areas.Customer.Controllers;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ControllerTesting
{
    [TestFixture]
    public class HomeControllerTest
    {
     
        [Test]
        [Fact]
        public void IndexShouldApplyReturnDefaultView()
        {
            string conn = "Server=LAPTOP-FB6CVF3F; Database=OnlineShop;TrustServerCertificate=true; Trusted_Connection=True; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(conn);

            var _db = new ApplicationDbContext(optionsBuilder.Options);
            var systemUnderTesting = new HomeController(_db);
            var result = systemUnderTesting.Index(1, "moni") as ViewResult;
            NUnit.Framework.Assert.That(result?.ViewName, Is.SameAs("NotFound"));
        }

        [Test]
        [Fact]
        public void IndexShouldApplyReturnProductView()
        {
            string conn = "Server=LAPTOP-FB6CVF3F; Database=OnlineShop;TrustServerCertificate=true; Trusted_Connection=True; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(conn);

            var _db = new ApplicationDbContext(optionsBuilder.Options);
            var systemUnderTesting = new HomeController(_db);
            var result = systemUnderTesting.Index(1, "A") as ViewResult;
            NUnit.Framework.Assert.That(result?.Model, Is.Not.Null);
        }


        [Test]
        [Fact]
        public void DetailShouldApplyReturnDefaultView()
        {
            string conn = "Server=LAPTOP-FB6CVF3F; Database=OnlineShop;TrustServerCertificate=true; Trusted_Connection=True; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(conn);

            var _db = new ApplicationDbContext(optionsBuilder.Options);
            var systemUnderTesting = new HomeController(_db);
            var result = systemUnderTesting.Details(4343) as ViewResult;
            NUnit.Framework.Assert.That(result?.ViewName, Is.Null);
        }


        [Test]
        [Fact]
        public void DetailShouldApplyReturnProductView()
        {
            string conn = "Server=LAPTOP-FB6CVF3F; Database=OnlineShop;TrustServerCertificate=true; Trusted_Connection=True; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(conn);

            var _db = new ApplicationDbContext(optionsBuilder.Options);
            var systemUnderTesting = new HomeController(_db);
            var result = systemUnderTesting.Details(2024) as ViewResult;
            NUnit.Framework.Assert.That(result?.Model, Is.Not.Null);
        }
    }
}
