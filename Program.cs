using System;
using Microsoft.EntityFrameworkCore;

namespace Newton
{
    class Newton
    {
        // Ingångspunkt för applikationen
        static void Main(string[] args)
        {
            // Skapa en instans av Newton-klassen som kommer att hantera bibliotekssystemet
            Newton libraryManager = new Newton();
            
            // Anropa ManageLibrary-metoden för att starta bibliotekssystemet
            libraryManager.ManageLibrary();
        }

        // Denna metod hanterar huvudmenyn för bibliotekssystemet
        public void ManageLibrary()
        {
            // Visa tillgängliga alternativ för användaren
            Console.WriteLine("1: Lägg till författare");
            Console.WriteLine("2: Lägg till bok");
            Console.WriteLine("3: Lägg till bok-författare relation");
            Console.WriteLine("4: Lägg till lån");
            Console.WriteLine("5: Ta bort författare");
            Console.WriteLine("6: Ta bort bok");
            Console.WriteLine("7: Ta bort lån");
            Console.WriteLine("8: Lista alla böcker med författare");
            Console.WriteLine("9: Lista alla lån med återlämningsdatum");
            Console.WriteLine("0: Avsluta");

            // Oändlig loop som håller programmet igång tills användaren väljer att avsluta
            while (true)
            {
                Console.Write("\nAnge ditt val: ");
                string choice = Console.ReadLine();  // Läs användarens val

                try
                {
                    // Switch-sats för att hantera användarens val
                    switch (choice)
                    {
                        // Alternativ 1: Lägg till en ny författare
                        case "1":
                            Console.Write("Ange fullständigt namn: ");
                            string name = Console.ReadLine();  // Läs författarens fullständiga namn
                            Console.Write("Ange födelsedatum (yyyy-mm-dd): ");
                            DateTime birthDate = DateTime.Parse(Console.ReadLine());  // Parsar födelsedatum från användarens inmatning
                            AddAuthor(name, birthDate);  // Anropa AddAuthor-metoden för att lägga till författaren i databasen
                            break;

                        // Alternativ 2: Lägg till en ny bok
                        case "2":
                            Console.Write("Ange titel: ");
                            string title = Console.ReadLine();  // Läs bokens titel
                            Console.Write("Ange genre: ");
                            string genre = Console.ReadLine();  // Läs bokgenren
                            Console.Write("Ange publiceringsår: ");
                            int publicationYear = int.Parse(Console.ReadLine());  // Parsar publiceringsåret
                            AddBook(title, genre, publicationYear);  // Anropa AddBook-metoden för att lägga till boken i databasen
                            break;

                        // Alternativ 3: Lägg till en bok-författare relation
                        case "3":
                            Console.Write("Ange BookID: ");
                            int bookId = int.Parse(Console.ReadLine());  // Läs BookID
                            Console.Write("Ange AuthorID: ");
                            int authorId = int.Parse(Console.ReadLine());  // Läs AuthorID
                            AddBookAuthorRelation(bookId, authorId);  // Anropa AddBookAuthorRelation-metoden för att skapa relationen
                            break;

                        // Alternativ 4: Lägg till ett lån
                        case "4":
                            Console.Write("Ange BookID: ");
                            int loanBookId = int.Parse(Console.ReadLine());  // Läs BookID för lånet
                            Console.Write("Ange läsarnamn: ");
                            string readerName = Console.ReadLine();  // Läs läsarens namn
                            Console.Write("Ange lånedatum (yyyy-mm-dd): ");
                            DateTime loanDate = DateTime.Parse(Console.ReadLine());  // Parsar lånedatumet
                            AddLoan(loanBookId, readerName, loanDate);  // Anropa AddLoan-metoden för att registrera lånet
                            break;

                        // Alternativ 5: Ta bort en författare
                        case "5":
                            Console.Write("Ange AuthorID för att ta bort: ");
                            int deleteAuthorId = int.Parse(Console.ReadLine());  // Läs AuthorID för att ta bort
                            DeleteAuthor(deleteAuthorId);  // Anropa DeleteAuthor-metoden för att ta bort författaren från databasen
                            break;

                        // Alternativ 6: Ta bort en bok
                        case "6":
                            Console.Write("Ange BookID för att ta bort: ");
                            int deleteBookId = int.Parse(Console.ReadLine());  // Läs BookID för att ta bort
                            DeleteBook(deleteBookId);  // Anropa DeleteBook-metoden för att ta bort boken från databasen
                            break;

                        // Alternativ 7: Ta bort ett lån
                        case "7":
                            Console.Write("Ange LoanID för att ta bort: ");
                            int deleteLoanId = int.Parse(Console.ReadLine());  // Läs LoanID för att ta bort
                            DeleteLoan(deleteLoanId);  // Anropa DeleteLoan-metoden för att ta bort lånet från databasen
                            break;

                        // Alternativ 8: Lista alla böcker med deras författare
                        case "8":
                            ListAllBooksWithAuthors();  // Anropa ListAllBooksWithAuthors-metoden för att visa alla böcker och deras författare
                            break;

                        // Alternativ 9: Lista alla lån med återlämningsdatum
                        case "9":
                            ListAllLoansWithReturnDates();  // Anropa ListAllLoansWithReturnDates-metoden för att visa alla lån och deras återlämningsdatum
                            break;

                        // Alternativ 0: Avsluta programmet
                        case "0":
                            Console.WriteLine("Avslutar...");
                            return;  // Avsluta loopen och programmet

                        // Standardfall: Hantera ogiltig inmatning
                        default:
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    // Fångar eventuella formatfel (t.ex. ogiltigt datum eller nummerinmatning)
                    Console.WriteLine($"Inmatningsfel: {ex.Message}. Vänligen ange värdena korrekt.");
                }
                catch (Exception ex)
                {
                    // Fångar andra oförutsedda fel
                    Console.WriteLine($"Ett oväntat fel inträffade: {ex.Message}. Försök igen.");
                }
            }
        }

        // Metod för att lägga till en ny författare i databasen
        public void AddAuthor(string name, DateTime birthDate)
        {
            using (var context = new AppDbContext())  // Använd AppDbContext för att interagera med databasen
            {
                var author = new Author  // Skapa ett nytt Author-objekt
                {
                    Name = name,
                    BirthDate = birthDate
                };

                context.Authors.Add(author);  // Lägg till författaren i Authors-tabellen
                context.SaveChanges();  // Spara ändringarna i databasen
                Console.WriteLine($"Författare {name} tillagd.");
            }
        }

        // Metod för att lägga till en ny bok i databasen
        public void AddBook(string title, string genre, int publicationYear)
        {
            using (var context = new AppDbContext())
            {
                var book = new Book  // Skapa ett nytt Book-objekt
                {
                    Title = title,
                    Genre = genre,
                    PublicationYear = publicationYear
                };

                context.Books.Add(book);  // Lägg till boken i Books-tabellen
                context.SaveChanges();  // Spara ändringarna i databasen
                Console.WriteLine($"Bok '{title}' tillagd.");
            }
        }

        // Metod för att lägga till en bok-författare relation i databasen
        public void AddBookAuthorRelation(int bookId, int authorId)
        {
            using (var context = new AppDbContext())
            {
                var relation = new BookAuthor  // Skapa en ny BookAuthor-relation
                {
                    BookID = bookId,
                    AuthorID = authorId
                };

                context.BookAuthors.Add(relation);  // Lägg till relationen i BookAuthors-tabellen
                context.SaveChanges();  // Spara ändringarna i databasen
                Console.WriteLine($"Relation mellan BookID {bookId} och AuthorID {authorId} tillagd.");
            }
        }

        // Metod för att lägga till ett nytt lån i databasen
        public void AddLoan(int bookId, string readerName, DateTime loanDate)
        {
            using (var context = new AppDbContext())
            {
                var loan = new Loan  // Skapa ett nytt Loan-objekt
                {
                    BookID = bookId,
                    ReaderName = readerName,
                    LoanDate = loanDate,
                    ReturnDate = null  // Lånet är fortfarande pågående, så återlämningsdatum är null
                };

                context.Loans.Add(loan);  // Lägg till lånet i Loans-tabellen
                context.SaveChanges();  // Spara ändringarna i databasen
                Console.WriteLine($"Lån för BookID {bookId} tillagd för {readerName}.");
            }
        }

        // Metod för att ta bort en författare från databasen
        public void DeleteAuthor(int authorId)
        {
            using (var context = new AppDbContext())
            {
                var author = context.Authors.Find(authorId);  // Hitta författaren baserat på AuthorID
                if (author != null)
                {
                    context.Authors.Remove(author);  // Ta bort författaren från Authors-tabellen
                    context.SaveChanges();  // Spara ändringarna i databasen
                    Console.WriteLine($"Författare med ID {authorId} borttagen.");
                }
                else
                {
                    Console.WriteLine("Författare inte funnen.");
                }
            }
        }

        // Metod för att ta bort en bok från databasen
        public void DeleteBook(int bookId)
        {
            using (var context = new AppDbContext())
            {
                var book = context.Books.Find(bookId);  // Hitta boken baserat på BookID
                if (book != null)
                {
                    context.Books.Remove(book);  // Ta bort boken från Books-tabellen
                    context.SaveChanges();  // Spara ändringarna i databasen
                    Console.WriteLine($"Bok med ID {bookId} borttagen.");
                }
                else
                {
                    Console.WriteLine("Bok inte funnen.");
                }
            }
        }

        // Metod för att ta bort ett lån från databasen
        public void DeleteLoan(int loanId)
        {
            using (var context = new AppDbContext())
            {
                var loan = context.Loans.Find(loanId);  // Hitta lånet baserat på LoanID
                if (loan != null)
                {
                    context.Loans.Remove(loan);  // Ta bort lånet från Loans-tabellen
                    context.SaveChanges();  // Spara ändringarna i databasen
                    Console.WriteLine($"Lån med ID {loanId} borttaget.");
                }
                else
                {
                    Console.WriteLine("Lån inte funnet.");
                }
            }
        }

        // Metod för att lista alla böcker tillsammans med deras författare
        public void ListAllBooksWithAuthors()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author);  // Hämta alla böcker med författare
                foreach (var book in books)
                {
                    Console.WriteLine($"Bok: {book.Title}, Genre: {book.Genre}");
                    foreach (var ba in book.BookAuthors)  // Iterera igenom varje bok och visa författarna
                    {
                        Console.WriteLine($"  Författare: {ba.Author.Name}");
                    }
                }
            }
        }

        // Metod för att lista alla lån med deras återlämningsdatum
        public void ListAllLoansWithReturnDates()
        {
            using (var context = new AppDbContext())
            {
                var loans = context.Loans.Include(l => l.Book);  // Hämta alla lån med böcker
                foreach (var loan in loans)
                {
                    // Visa låneinformation och visa "Inte återlämnad" om inget återlämningsdatum finns
                    Console.WriteLine($"Lån ID: {loan.LoanID}, Bok: {loan.Book.Title}, Läsares namn: {loan.ReaderName}, Lånedatum: {loan.LoanDate}, Återlämningsdatum: {loan.ReturnDate?.ToString() ?? "Inte återlämnad"}");
                }
            }
        }
    }
}
