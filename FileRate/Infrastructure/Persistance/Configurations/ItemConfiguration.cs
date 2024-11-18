using Domain.Entities.Items;
using Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> item)
    {
        item.HasKey(i => i.Id);

        item.Property(i => i.Id).HasConversion(
            itemId => itemId.Value,
            value => new ItemId(value));

        item.Property(i => i.MemberId).HasConversion(
            memberId => memberId.Value,
            value => new MemberId(value));

        item.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);

        item.Property(i => i.Description)
            .HasMaxLength(200);

        item.Property(i => i.File)
            .IsRequired()
            .HasColumnType("varbinary(max)");

        item.Property(i => i.CreatedAt)
            .IsRequired();

        item.Property(i => i.UpdatedAt)
            .IsRequired();

        item.HasMany(i => i.Keywords)
            .WithMany(k => k.Items);

        item.HasOne(i => i.Member)
            .WithMany(m => m.Items)
            .HasForeignKey(i => i.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
