using System;
using System.Collections.Generic;

public class Author
{
    public int AuthorID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }

    // Många-till-många-relation med Book
    public ICollection<BookAuthor> BookAuthors { get; set; }
}

public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public string ISBN { get; set; }

    // Många-till-många-relation med Author
    public ICollection<BookAuthor> BookAuthors { get; set; }

    // En-till-många-relation med Loan
    public ICollection<Loan> Loans { get; set; }
}

public class BookAuthor
{
    public int BookID { get; set; }
    public int AuthorID { get; set; }

    // Navigation properties
    public Book Book { get; set; }
    public Author Author { get; set; }
}

public class Loan
{
    public int LoanID { get; set; }
    public int BookID { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }  // Nullable, eftersom boken kanske inte har återlämnats
    public string ReaderName { get; set; }

    // Navigation property
    public Book Book { get; set; }
}
