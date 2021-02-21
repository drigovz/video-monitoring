using Bogus;
using System;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Test.Builders
{
    public class VideoBuilder
    {
        static Faker faker = new Faker();

        public string Description = faker.Lorem.Paragraph();
        public string File = faker.Image.LoremFlickrUrl();
        public string FileName = faker.Name.FirstName();
        public int Size = faker.Random.Int();
        public Guid ServerId = new Guid();

        public static VideoBuilder New()
        {
            return new VideoBuilder();
        }

        public VideoBuilder VideoWithDescription(string description)
        {
            Description = description;
            return this;
        }

        public VideoBuilder VideoWithFile(string file)
        {
            File = file;
            return this;
        }

        public VideoBuilder VideoWithFileName(string fileName)
        {
            FileName = fileName;
            return this;
        }

        public VideoBuilder VideoWithSize(int size)
        {
            Size = size;
            return this;
        }

        public VideoBuilder VideoWithServer(Guid server)
        {
            ServerId = server;
            return this;
        }

        public Video Build()
        {
            return new Video(Description, File, FileName, Size, ServerId);
        }
    }
}
