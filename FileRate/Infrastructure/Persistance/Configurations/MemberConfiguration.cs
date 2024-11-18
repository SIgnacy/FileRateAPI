using Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> member)
    {
        member.HasKey(m => m.Id);

        member.Property(m => m.Id).HasConversion(
            memberId => memberId.Value,
            value => new MemberId(value));

        member.Property(m => m.Username)
            .IsRequired()
            .HasColumnType("varchar(30)");

        member.Property(m => m.DisplayName)
            .HasMaxLength(30);

        member.HasIndex(m => m.Username).IsUnique();

        member.HasMany(m => m.Ratings)
            .WithOne(r => r.Member)
            .HasForeignKey(r => r.MemberId)
            .OnDelete(DeleteBehavior.NoAction);

        member.HasMany(m => m.Items)
            .WithOne(i => i.Member)
            .HasForeignKey(i => i.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
