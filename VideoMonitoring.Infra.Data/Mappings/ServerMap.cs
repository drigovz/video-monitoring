using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Infra.Data.Mappings
{
    public class ServerMap : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.ToTable("Server");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Ip)
                    .IsRequired();

            builder.Property(x => x.Port)
                    .IsRequired()
                    .HasMaxLength(10);
        }
    }
}
