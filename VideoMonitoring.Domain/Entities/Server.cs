using System;
using System.Linq;

namespace VideoMonitoring.Domain.Entities
{
    public class Server : BaseEntity
    {
        public string Name { get; private set; }
        public string Ip { get; private set; }
        public int Port { get; private set; }

        public Server()
        {
        }

        public Server(string name, string ip, int port)
        {
            IpAddressValidation(ip);
            Validations(name, ip, port);

            Name = name;
            Ip = ip;
            Port = port;
        }

        private bool IpAddressValidation(string ip)
        {
            if (String.IsNullOrWhiteSpace(ip))
                return false;

            string[] splitValues = ip.Split('.');

            if (splitValues.Length != 4)
                return false;

            byte parsingValues;

            return splitValues.All(ipValue => byte.TryParse(ipValue, out parsingValues));
        }

        private void Validations(string name, string ip, int port)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required!");

            if (name.Length <= 2)
                throw new ArgumentException("Name length must contain more than two characters!");

            if (port <= 0)
                throw new ArgumentException("Port not valid!");

            if (!IpAddressValidation(ip))
                throw new ArgumentException("IP address not valid!");
        }
    }
}
