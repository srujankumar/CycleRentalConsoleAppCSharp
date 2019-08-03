using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Unit_Tests
{
    public class Class1
    {
        [Fact]
        public void Test()
        {
            var result = "test";
            result.Should().Be("test");
        }
    }
}
