using System;
using System.Collections.Generic;

public class Author
{
    // Unique identifier for the author
    public int AuthorID { get; set; }

    // Full name of the author (combined first and last name)
    public string Name { get; set; }

    // Birth date of the author
    public DateTime BirthDate { get; set; }

    // Many-to-many relationship with Book through the BookAuthor table
    public ICollection<BookAuthor> BookAuthors { get; set; }
}

public class Book
{
    // Unique identifier for the book
    public int BookID { get; set; }

    // Title of the book
    public string Title { get; set; }

    // Genre of the book (e.g., Fiction, Non-fiction)
    public string Genre { get; set; }

    // Publication year of the book
    public int PublicationYear { get; set; }

    // Many-to-many relationship with Author through the BookAuthor table
    public ICollection<BookAuthor> BookAuthors { get; set; }

    // One-to-many relationship with Loan table
    public ICollection<Loan> Loans { get; set; }
}

public class BookAuthor
{
    // Foreign key to the Book entity
    public int BookID { get; set; }

    // Foreign key to the Author entity
    public int AuthorID { get; set; }

    // Navigation property for the related Book
    public Book Book { get; set; }

    // Navigation property for the related Author
    public Author Author { get; set; }
}

public class Loan
{
    // Unique identifier for the loan
    public int LoanID { get; set; }

    // Foreign key to the Book entity
    public int BookID { get; set; }

    // Date when the book was loaned
    public DateTime LoanDate { get; set; }

    // Date when the book was returned (nullable in case it's not returned yet)
    public DateTime? ReturnDate { get; set; }

    // Name of the reader who loaned the book
    public string ReaderName { get; set; }

    // Navigation property for the related Book
    public Book Book { get; set; }
}
