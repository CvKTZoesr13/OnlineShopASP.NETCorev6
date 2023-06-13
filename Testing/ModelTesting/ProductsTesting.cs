using NUnit.Framework;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ModelTesting
{
    [TestFixture]
    public class ProductsTesting
    {
        [Test]
        [Fact]
        public void TestProductName()
        {
            Products p = new();
            p.Name = "  Lenovo    ";
            var check = new ProductsValid();
            var result = check.CheckProductName(p);
            NUnit.Framework.Assert.That(result, Is.True);
        }

    }
}
