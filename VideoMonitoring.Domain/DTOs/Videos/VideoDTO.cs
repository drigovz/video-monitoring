using System;
using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.Domain.DTOs.Videos
{
    public class VideoDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        public string File { get; set; }

        public string FileName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Size file not valid!")]
        public int Size { get; set; }

        public Guid ServerId { get; set; }
    }
}
