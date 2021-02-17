using System;
using VideoMonitoring.Test.Builders;
using Xunit;

namespace VideoMonitoring.Test.VideoMonitoring.Domain.Test
{
    public class ServerTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Not_Accept_Invalid_Name(string invalidNames)
        {
            Assert.Throws<ArgumentException>(() =>
                ServerBuilder.New().ServerWithName(invalidNames).Build()
            )
            .AssertThrowsWithMessage("Name is required!");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        public void Should_Not_Accept_Name_With_Less_Than_Two_Chars(string invalidNames)
        {
            Assert.Throws<ArgumentException>(() =>
                ServerBuilder.New().ServerWithName(invalidNames).Build()
            )
            .AssertThrowsWithMessage("Name length must contain more than two characters!");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Not_Accept_Invalid_Ports(int invalidPorts)
        {
            Assert.Throws<ArgumentException>(() =>
                ServerBuilder.New().ServerWithPort(invalidPorts).Build()
            )
            .AssertThrowsWithMessage("Port not valid!");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.2")]
        [InlineData("1.2.3")]
        [InlineData("1.2.3.4.5")]
        [InlineData("12345.2.3.4")]
        [InlineData("1.23456.3.4")]
        [InlineData("1.2.34567.4")]
        [InlineData("1.2.3.45678")]
        public void Should_Not_Accept_Invalid_IP_Addres(string invalidIps)
        {
            Assert.Throws<ArgumentException>(() =>
                ServerBuilder.New().ServerWithIp(invalidIps).Build()
            )
            .AssertThrowsWithMessage("IP address not valid!");
        }
    }
}
