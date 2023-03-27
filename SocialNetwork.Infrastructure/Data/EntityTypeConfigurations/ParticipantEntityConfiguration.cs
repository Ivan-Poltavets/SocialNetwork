using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Entities.Chat;

namespace SocialNetwork.Infrastructure.Data.EntityTypeConfigurations;

internal class ParticipantEntityConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasKey(x => x.ChatId);
    }
}
