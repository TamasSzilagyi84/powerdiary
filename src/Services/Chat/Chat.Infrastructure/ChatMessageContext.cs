namespace Chat.Infrastructure
{
    using Domain.Models;

    using EntityConfiguration;

    using Microsoft.EntityFrameworkCore;

    public class ChatMessageContext : DbContext
    {
        public ChatMessageContext(DbContextOptions<ChatMessageContext> options) : base(options)
        {
            
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("chat");
            modelBuilder.ApplyConfiguration(new ChatMessageEntityTypeConfiguration());
        }
    }
}
