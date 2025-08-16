using DinoSkore.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DinoSkore.Api.Database.Configurations;

public sealed class CompetitionCongfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasMaxLength(500);
        builder.Property(h => h.UserId).HasMaxLength(500);

        builder.Property(h => h.Name).HasMaxLength(100);

        builder.Property(h => h.Description).HasMaxLength(500);
    }
}