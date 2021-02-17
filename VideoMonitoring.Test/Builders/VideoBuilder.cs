﻿using Bogus;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Test.Builders
{
    public class VideoBuilder
    {
        static Faker faker = new Faker();

        public string Description = faker.Lorem.Paragraph();
        public string File = faker.Image.LoremFlickrUrl();
        public int Size = faker.Random.Int();
        public int ServerId = faker.Random.Int();

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

        public VideoBuilder VideoWithSize(int size)
        {
            Size = size;
            return this;
        }

        public VideoBuilder VideoWithServer(int server)
        {
            ServerId = server;
            return this;
        }

        public Video Build()
        {
            return new Video(Description, File, Size, ServerId);
        }
    }
}