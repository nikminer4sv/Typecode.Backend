using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Typecode.Domain;

namespace Typecode.Persistence.EntityTypeConfigurations;

public class FinishedTestConfiguration : IEntityTypeConfiguration<FinishedTest>
{
    public void Configure(EntityTypeBuilder<FinishedTest> builder)
    {
        builder.HasKey(x => x.Id);
    }
}