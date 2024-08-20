

using Microsoft.EntityFrameworkCore;
using Models;

namespace Context;
public class FlashcardContext : DbContext
{
    public FlashcardContext(DbContextOptions<FlashcardContext> options)
            : base(options)
    {
    }
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<StudySet> StudySets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<Flashcard>().ToTable("Flashcard");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<StudySet>(
            entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever();
            });
        modelBuilder.Entity<StudySet>().ToTable("StudySet");
    }

}