using System;
using Microsoft.EntityFrameworkCore;

class LibraryManager
{
    public void ManageLibrary()
    {
        // Display menu options for library management
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
            // Prompt user for their choice
            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Adding a new author
                    Console.Write("Enter Full Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Birth Date (yyyy-mm-dd): ");
                    DateTime birthDate = DateTime.Parse(Console.ReadLine());
                    AddAuthor(name, birthDate);
                    break;

                case "2":
                    // Adding a new book
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Genre: ");
                    string genre = Console.ReadLine();
                    Console.Write("Enter Publication Year: ");
                    int publicationYear = int.Parse(Console.ReadLine());
                    AddBook(title, genre, publicationYear);
                    break;

                case "3":
                    // Creating a relation between a book and an author
                    Console.Write("Enter BookID: ");
                    int bookId = int.Parse(Console.ReadLine());
                    Console.Write("Enter AuthorID: ");
                    int authorId = int.Parse(Console.ReadLine());
                    AddBookAuthorRelation(bookId, authorId);
                    break;

                case "4":
                    // Adding a loan for a book
                    Console.Write("Enter BookID: ");
                    int loanBookId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Reader Name: ");
                    string readerName = Console.ReadLine();
                    Console.Write("Enter Loan Date (yyyy-mm-dd): ");
                    DateTime loanDate = DateTime.Parse(Console.ReadLine());
                    AddLoan(loanBookId, readerName, loanDate);
                    break;

                case "5":
                    // Deleting an author
                    Console.Write("Enter AuthorID to delete: ");
                    int deleteAuthorId = int.Parse(Console.ReadLine());
                    DeleteAuthor(deleteAuthorId);
                    break;

                case "6":
                    // Deleting a book
                    Console.Write("Enter BookID to delete: ");
                    int deleteBookId = int.Parse(Console.ReadLine());
                    DeleteBook(deleteBookId);
                    break;

                case "7":
                    // Deleting a loan
                    Console.Write("Enter LoanID to delete: ");
                    int deleteLoanId = int.Parse(Console.ReadLine());
                    DeleteLoan(deleteLoanId);
                    break;

                case "8":
                    // Listing all books with their authors
                    ListAllBooksWithAuthors();
                    break;

                case "9":
                    // Listing all loans with their return dates
                    ListAllLoansWithReturnDates();
                    break;

                case "0":
                    // Exiting the program
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    // Handling invalid menu options
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Adds a new author to the database
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

    // Adds a new book to the database
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

    // Creates a relationship between a book and an author
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

    // Adds a loan record for a book
    public void AddLoan(int bookId, string readerName, DateTime loanDate)
    {
        using (var context = new AppDbContext())
        {
            var loan = new Loan
            {
                BookID = bookId,
                ReaderName = readerName,
                LoanDate = loanDate,
                ReturnDate = null // Return date is null until the book is returned
            };

            context.Loans.Add(loan);
            context.SaveChanges();
            Console.WriteLine($"Loan for BookID {bookId} added for {readerName}.");
        }
    }

    // Deletes an author record from the database
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

    // Deletes a book record from the database
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

    // Deletes a loan record from the database
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

    // Lists all books along with their associated authors
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

    // Lists all loans along with their return dates
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
