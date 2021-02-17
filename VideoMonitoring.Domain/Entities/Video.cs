﻿using System;

namespace VideoMonitoring.Domain.Entities
{
    public class Video : BaseEntity
    {
        public string Description { get; private set; }
        public string File { get; private set; }
        public int Size { get; private set; }
        public int ServerId { get; set; }

        public Video()
        {
        }

        public Video(string description, string file, int size, int serverId)
        {
            Validations(description, file, size, serverId);

            Description = description;
            File = file;
            Size = size;
            ServerId = serverId;
        }

        private void Validations(string description, string file, int size, int serverId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description is required!");

            if (string.IsNullOrEmpty(file))
                throw new ArgumentException("File is required!");

            if (size <= 0)
                throw new ArgumentException("Size file not valid!");

            if (serverId <= 0)
                throw new ArgumentException("Server not valid!");
        }
    }
}