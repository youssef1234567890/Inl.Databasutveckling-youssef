using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    // DbSets som representerar tabeller i databasen
    public DbSet<Author> Authors { get; set; }  // Tabell för författare
    public DbSet<Book> Books { get; set; }  // Tabell för böcker
    public DbSet<BookAuthor> BookAuthors { get; set; }  // Tabell för relationen mellan böcker och författare (many-to-many)
    public DbSet<Loan> Loans { get; set; }  // Tabell för lån

    // Metod för att konfigurera databasanslutningen och andra alternativ
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Här anger vi att vi använder SQL Server och ger anslutningssträngen till databasen
        optionsBuilder.UseSqlServer("Server=DESKTOP-JFN5NF3;Database=EFCoreDemoDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    // Metod för att konfigurera relationer mellan modeller, index och eventuella restriktioner
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definiera många-till-många relation mellan Book och Author med hjälp av BookAuthor som join-tabell
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookID, ba.AuthorID });  // Sätt ihop BookID och AuthorID till en sammansatt primärnyckel för join-tabellen

        // Definiera relationen mellan BookAuthor och Book (en Book kan ha flera relaterade Author)
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)  // En Book kan ha många BookAuthors
            .WithMany(b => b.BookAuthors)  // En Book har många relaterade BookAuthors
            .HasForeignKey(ba => ba.BookID);  // Anger att BookID är en främmande nyckel i BookAuthor-tabellen

        // Definiera relationen mellan BookAuthor och Author (en Author kan ha flera relaterade Books)
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)  // En Author kan ha många BookAuthors
            .WithMany(a => a.BookAuthors)  // En Author har många relaterade BookAuthors
            .HasForeignKey(ba => ba.AuthorID);  // Anger att AuthorID är en främmande nyckel i BookAuthor-tabellen

        // Definiera en-till-många relation mellan Book och Loan (en Book kan ha många lån)
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)  // Ett lån är kopplat till en bok
            .WithMany(b => b.Loans)  // En bok kan ha många lån
            .HasForeignKey(l => l.BookID);  // Anger att BookID är en främmande nyckel i Loan-tabellen
    }
}
