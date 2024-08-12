

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>().ToTable("Flashcard");
    }
}