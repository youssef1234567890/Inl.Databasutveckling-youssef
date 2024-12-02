using System;
using Microsoft.EntityFrameworkCore;

namespace Newton
{
    class Newton
    {
        // Entry point for the application
        static void Main(string[] args)
        {
            // Create an instance of Newton and start the library management system
            Newton libraryManager = new Newton();
            libraryManager.ManageLibrary();
        }

        public void ManageLibrary()
        {
            Console.WriteLine("1: Add Author");
            Console.WriteLine("2: Add Book");
            Console.WriteLine("3: Add Book-Author Relation");
            Console.WriteLine("4: Add Loan");
            Console.WriteLine("5: Delete Author");
            Console.WriteLine("6: Delete Book");
            Console.WriteLine("7: Delete Loan");
            Console.WriteLine("8: List All Books with Authors");
            Console.WriteLine("9: List All Loans with Return Dates");
            Console.WriteLine("0: Exit");

            while (true)
            {
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Full Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Birth Date (yyyy-mm-dd): ");
                        DateTime birthDate = DateTime.Parse(Console.ReadLine());
                        AddAuthor(name, birthDate);
                        break;

                    case "2":
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Genre: ");
                        string genre = Console.ReadLine();
                        Console.Write("Enter Publication Year: ");
                        int publicationYear = int.Parse(Console.ReadLine());
                        AddBook(title, genre, publicationYear);
                        break;

                    case "3":
                        Console.Write("Enter BookID: ");
                        int bookId = int.Parse(Console.ReadLine());
                        Console.Write("Enter AuthorID: ");
                        int authorId = int.Parse(Console.ReadLine());
                        AddBookAuthorRelation(bookId, authorId);
                        break;

                    case "4":
                        Console.Write("Enter BookID: ");
                        int loanBookId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Reader Name: ");
                        string readerName = Console.ReadLine();
                        Console.Write("Enter Loan Date (yyyy-mm-dd): ");
                        DateTime loanDate = DateTime.Parse(Console.ReadLine());
                        AddLoan(loanBookId, readerName, loanDate);
                        break;

                    case "5":
                        Console.Write("Enter AuthorID to delete: ");
                        int deleteAuthorId = int.Parse(Console.ReadLine());
                        DeleteAuthor(deleteAuthorId);
                        break;

                    case "6":
                        Console.Write("Enter BookID to delete: ");
                        int deleteBookId = int.Parse(Console.ReadLine());
                        DeleteBook(deleteBookId);
                        break;

                    case "7":
                        Console.Write("Enter LoanID to delete: ");
                        int deleteLoanId = int.Parse(Console.ReadLine());
                        DeleteLoan(deleteLoanId);
                        break;

                    case "8":
                        ListAllBooksWithAuthors();
                        break;

                    case "9":
                        ListAllLoansWithReturnDates();
                        break;

                    case "0":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void AddAuthor(string name, DateTime birthDate)
        {
            using (var context = new AppDbContext())
            {
                var author = new Author
                {
                    Name = name,
                    BirthDate = birthDate
                };

                context.Authors.Add(author);
                context.SaveChanges();
                Console.WriteLine($"Author {name} added successfully.");
            }
        }

        public void AddBook(string title, string genre, int publicationYear)
        {
            using (var context = new AppDbContext())
            {
                var book = new Book
                {
                    Title = title,
                    Genre = genre,
                    PublicationYear = publicationYear
                };

                context.Books.Add(book);
                context.SaveChanges();
                Console.WriteLine($"Book '{title}' added successfully.");
            }
        }

        public void AddBookAuthorRelation(int bookId, int authorId)
        {
            using (var context = new AppDbContext())
            {
                var relation = new BookAuthor
                {
                    BookID = bookId,
                    AuthorID = authorId
                };

                context.BookAuthors.Add(relation);
                context.SaveChanges();
                Console.WriteLine($"Relation added between BookID {bookId} and AuthorID {authorId}.");
            }
        }

        public void AddLoan(int bookId, string readerName, DateTime loanDate)
        {
            using (var context = new AppDbContext())
            {
                var loan = new Loan
                {
                    BookID = bookId,
                    ReaderName = readerName,
                    LoanDate = loanDate,
                    ReturnDate = null
                };

                context.Loans.Add(loan);
                context.SaveChanges();
                Console.WriteLine($"Loan for BookID {bookId} added for {readerName}.");
            }
        }

        public void DeleteAuthor(int authorId)
        {
            using (var context = new AppDbContext())
            {
                var author = context.Authors.Find(authorId);
                if (author != null)
                {
                    context.Authors.Remove(author);
                    context.SaveChanges();
                    Console.WriteLine($"Author with ID {authorId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Author not found.");
                }
            }
        }

        public void DeleteBook(int bookId)
        {
            using (var context = new AppDbContext())
            {
                var book = context.Books.Find(bookId);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                    Console.WriteLine($"Book with ID {bookId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
        }

        public void DeleteLoan(int loanId)
        {
            using (var context = new AppDbContext())
            {
                var loan = context.Loans.Find(loanId);
                if (loan != null)
                {
                    context.Loans.Remove(loan);
                    context.SaveChanges();
                    Console.WriteLine($"Loan with ID {loanId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Loan not found.");
                }
            }
        }

        public void ListAllBooksWithAuthors()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author);
                foreach (var book in books)
                {
                    Console.WriteLine($"Book: {book.Title}, Genre: {book.Genre}");
                    foreach (var ba in book.BookAuthors)
                    {
                        Console.WriteLine($"  Author: {ba.Author.Name}");
                    }
                }
            }
        }

        public void ListAllLoansWithReturnDates()
        {
            using (var context = new AppDbContext())
            {
                var loans = context.Loans.Include(l => l.Book);
                foreach (var loan in loans)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanID}, Book: {loan.Book.Title}, Reader: {loan.ReaderName}, Loan Date: {loan.LoanDate}, Return Date: {loan.ReturnDate?.ToString() ?? "Not Returned"}");
                }
            }
        }
    }
}
