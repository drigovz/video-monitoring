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

        [MinLength(1, ErrorMessage = "Port not valid!")]
        public string Ip { get; set; }

        //[RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP address not valid!")]
        public int Port { get; set; }
    }
}
