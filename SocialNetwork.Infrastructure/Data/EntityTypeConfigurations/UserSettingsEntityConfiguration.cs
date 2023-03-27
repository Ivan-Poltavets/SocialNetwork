using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Entities;

namespace SocialNetwork.Infrastructure.Data.EntityTypeConfigurations;

internal class UserSettingsEntityConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(x => x.UserId);
    }
}
