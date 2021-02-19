using System;
using System.ComponentModel.DataAnnotations;

namespace VideoMonitoring.Domain.DTOs.Servers
{
    public class ServerDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Name length must contain more than two characters!")]
        public string Name { get; set; }

        public string Ip { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Port not valid!")]
        public int Port { get; set; }
    }
}
