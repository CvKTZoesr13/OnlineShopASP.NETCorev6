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
    public class SpecialTagsTests
    {
        [Test]
        [Fact]
        public void TestSpecialTag()
        {
            SpecialTags s = new();
            s.SpecialTag = "";
            var check = new SpecialTagValid();
            var result = check.CheckSpecialTagName(s);
            NUnit.Framework.Assert.That(result, Is.True);
        }

    }
}
