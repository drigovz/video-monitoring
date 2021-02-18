using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Infra.Data.Mappings
{
    public class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.File)
                   .IsRequired();
        }
    }
}
