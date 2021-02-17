using System;
using VideoMonitoring.Test.Builders;
using Xunit;

namespace VideoMonitoring.Test.VideoMonitoring.Domain.Test
{
    public class VideoTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Not_Accept_Invalid_Description(string invalidDescriptions)
        {
            Assert.Throws<ArgumentException>(() =>
                VideoBuilder.New().VideoWithDescription(invalidDescriptions).Build()
            )
            .AssertThrowsWithMessage("Description is required!");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Not_Accept_Invalid_File(string invalidFiles)
        {
            Assert.Throws<ArgumentException>(() =>
                VideoBuilder.New().VideoWithFile(invalidFiles).Build()
            )
            .AssertThrowsWithMessage("File is required!");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Not_Accept_Invalid_Size(int invalidSizes)
        {
            Assert.Throws<ArgumentException>(() =>
                VideoBuilder.New().VideoWithSize(invalidSizes).Build()
            )
            .AssertThrowsWithMessage("Size file not valid!");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_Not_Accept_Invalid_Server_Id(int invalidServers)
        {
            Assert.Throws<ArgumentException>(() =>
                VideoBuilder.New().VideoWithServer(invalidServers).Build()
            )
            .AssertThrowsWithMessage("Server not valid!");
        }
    }
}
