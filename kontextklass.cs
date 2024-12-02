using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    // DbSets representing tables in the database
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Loan> Loans { get; set; }

    // Configuring the database connection and other options
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use SQL Server and provide the connection string
        optionsBuilder.UseSqlServer("Server=DESKTOP-JFN5NF3;Database=EFCoreDemoDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    // Configuring model relationships, indexes, and any constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define many-to-many relationship between Book and Author using the BookAuthor join table
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookID, ba.AuthorID }); // Composite key for the join table

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookID); // Foreign key linking to Book

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorID); // Foreign key linking to Author

        // One-to-many relationship between Book and Loan
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookID); // Foreign key linking to Book
    }
}
