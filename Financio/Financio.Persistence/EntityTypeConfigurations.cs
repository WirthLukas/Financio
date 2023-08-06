using Financio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financio.Persistence;

internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Number);
        builder.Ignore(a => a.Id);
        builder.Property(a => a.Number)
            .HasMaxLength(4)
            .IsFixedLength();
        builder.Property(a => a.Name)
            .HasMaxLength(200);
        builder.Property(a => a.Description)
            .HasMaxLength(500);
        builder.Property(a => a.RowVersion).IsRowVersion();

        builder
            .HasMany<AccountReference>(a => a.References)
            .WithOne(ar => ar.Account)
            .HasForeignKey(ar => ar.AccountNumber)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(a => a.CounterAccountReferences)
            .WithMany(ar => ar.CounterAccounts)
            .UsingEntity(
                l => l.HasOne(typeof(AccountReference)).WithMany().OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Account)).WithMany().OnDelete(DeleteBehavior.NoAction)
            );
    }
}

internal class AccountReferenceEntityTypeConfiguration : IEntityTypeConfiguration<AccountReference>
{
    public void Configure(EntityTypeBuilder<AccountReference> builder)
    {
        builder.HasKey(ar => ar.Id);
        builder.Property(ar => ar.Side).HasConversion<string>();
        // builder.Property(ar => ar.Value);
        // builder.Property(ar => ar.Date);
    }
}