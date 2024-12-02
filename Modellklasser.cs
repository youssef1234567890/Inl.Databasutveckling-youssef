using System;
using System.Collections.Generic;

// Klass som representerar en författare
public class Author
{
    // Unikt ID för författaren
    public int AuthorID { get; set; }

    // Fullständigt namn på författaren (för- och efternamn)
    public string Name { get; set; }

    // Författarens födelsedatum
    public DateTime BirthDate { get; set; }

    // Många-till-många relation med Book genom BookAuthor-tabellen
    public ICollection<BookAuthor> BookAuthors { get; set; }  // Lista över relationer mellan författaren och böcker
}

// Klass som representerar en bok
public class Book
{
    // Unikt ID för boken
    public int BookID { get; set; }

    // Titel på boken
    public string Title { get; set; }

    // Genre för boken (t.ex. Skönlitteratur, Facklitteratur)
    public string Genre { get; set; }

    // Utgivningsår för boken
    public int PublicationYear { get; set; }

    // Många-till-många relation med Author genom BookAuthor-tabellen
    public ICollection<BookAuthor> BookAuthors { get; set; }  // Lista över relationer mellan boken och författare

    // En-till-många relation med Loan-tabellen
    public ICollection<Loan> Loans { get; set; }  // Lista över lån som är kopplade till boken
}

// Klass som representerar en relation mellan bok och författare
public class BookAuthor
{
    // Främmande nyckel till Book-entiteten
    public int BookID { get; set; }

    // Främmande nyckel till Author-entiteten
    public int AuthorID { get; set; }

    // Navigationsattribut för relaterad bok
    public Book Book { get; set; }  // Referens till den kopplade boken

    // Navigationsattribut för relaterad författare
    public Author Author { get; set; }  // Referens till den kopplade författaren
}

// Klass som representerar ett lån
public class Loan
{
    // Unikt ID för lånet
    public int LoanID { get; set; }

    // Främmande nyckel till Book-entiteten
    public int BookID { get; set; }

    // Datum då boken lånades ut
    public DateTime LoanDate { get; set; }

    // Datum då boken lämnades tillbaka (kan vara null om den inte har återlämnats)
    public DateTime? ReturnDate { get; set; }

    // Namn på läsaren som lånade boken
    public string ReaderName { get; set; }

    // Navigationsattribut för relaterad bok
    public Book Book { get; set; }  // Referens till den kopplade boken
}
