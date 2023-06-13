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
    public class ProductTypesTests
    {
        [Test]
        [Fact]
        public void TestProductType()
        {
            ProductTypes p = new();
            p.ProductType = "  Lenovo    ";
            var check = new ProductTypeValid();
            var result = check.CheckProductTypeName(p);
            NUnit.Framework.Assert.That(result, Is.True);
        }

    }
}
