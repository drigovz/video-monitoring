using Bogus;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Test.Builders
{
    public class ServerBuilder
    {
        static Faker faker = new Faker();

        public string Name = faker.Lorem.Sentence();
        public string Ip = faker.Internet.IpAddress().ToString();
        public int Port = faker.Internet.Port();

        public static ServerBuilder New()
        {
            return new ServerBuilder();
        }

        public ServerBuilder ServerWithName(string name)
        {
            Name = name;
            return this;
        }

        public ServerBuilder ServerWithIp(string ip)
        {
            Ip = ip;
            return this;
        }

        public ServerBuilder ServerWithPort(int port)
        {
            Port = port;
            return this;
        }

        public Server Build()
        {
            return new Server(Name, Ip, Port);
        }
    }
}
