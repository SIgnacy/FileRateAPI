using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> rating)
    {
        rating.HasKey(r => r.Id);

        rating.Property(r => r.Id).HasConversion(
            ratingId => ratingId.Value,
            value => new RatingId(value));

        rating.Property(r => r.ItemId).HasConversion(
            itemId => itemId.Value,
            value => new ItemId(value));

        rating.Property(r => r.MemberId).HasConversion(
            memberId => memberId.Value,
            value => new MemberId(value));

        rating.Property(r => r.Rate)
            .IsRequired();

        rating.Property(r => r.Comment)
            .HasMaxLength(200);

        rating.HasOne(r => r.Item)
            .WithMany(i => i.Rating)
            .HasForeignKey(r => r.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        rating.HasOne(r => r.Member)
            .WithMany(m => m.Ratings)
            .HasForeignKey(r => r.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        rating.ToTable(r => r.HasCheckConstraint("CK_Rate_Range", "Rate BETWEEN 1 AND 10"));
    }
}