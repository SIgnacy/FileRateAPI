using Domain.Entities.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> keyword)
    {
        keyword.HasKey(k => k.Id);

        keyword.Property(k => k.Id).HasConversion(
            keywordId => keywordId.Value,
            value => new KeywordId(value));

        keyword.Property(k => k.Word)
            .IsRequired()
            .HasMaxLength(50);

        keyword.HasMany(k => k.Items)
            .WithMany(i => i.Keywords);

        keyword.HasIndex(k => k.Word).IsUnique();
    }
}
