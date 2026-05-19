using LIBRARYMANAGEMENT.API.Data;
using LIBRARYMANAGEMENT.API.DTOs;
using LIBRARYMANAGEMENT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBRARYMANAGEMENT.API.Services;

public class BooksService : IBooksService
{
    private readonly LibraryDbContext _context;

    public BooksService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDto>> GetAllBooksAsync(string? category = null, string? author = null, string? title = null)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(b => b.Category.Contains(category));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        var books = await query.ToListAsync();
        return books.Select(MapToDto).ToList();
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book == null ? null : MapToDto(book);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto newBookDto)
    {
        if (await _context.Books.AnyAsync(b => b.ISBN == newBookDto.ISBN))
        {
            throw new InvalidOperationException("ISBN already exists.");
        }

        var book = new Book
        {
            Title = newBookDto.Title,
            Author = newBookDto.Author,
            ISBN = newBookDto.ISBN,
            Category = newBookDto.Category,
            Publisher = newBookDto.Publisher,
            YearPublished = newBookDto.YearPublished,
            TotalCopies = 1,
            AvailableCopies = 1,
            DateAdded = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return MapToDto(book);
    }

    public async Task<bool> UpdateBookAsync(int id, UpdateBookDto updatedBookDto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }

        if (book.ISBN != updatedBookDto.ISBN && await _context.Books.AnyAsync(b => b.ISBN == updatedBookDto.ISBN))
        {
            throw new InvalidOperationException("ISBN already exists.");
        }

        book.Title = updatedBookDto.Title;
        book.Author = updatedBookDto.Author;
        book.ISBN = updatedBookDto.ISBN;
        book.Category = updatedBookDto.Category;
        book.Publisher = updatedBookDto.Publisher;
        book.YearPublished = updatedBookDto.YearPublished;
        book.TotalCopies = updatedBookDto.TotalCopies;

        if (book.AvailableCopies > updatedBookDto.TotalCopies)
        {
            book.AvailableCopies = updatedBookDto.TotalCopies;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }

        var activeLoan = await _context.Loans.AnyAsync(l => l.BookId == id && l.ReturnDate == null);
        if (activeLoan)
        {
            throw new InvalidOperationException("Cannot delete a book that is currently on loan.");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Category = book.Category,
            Publisher = book.Publisher,
            YearPublished = book.YearPublished,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies,
            DateAdded = book.DateAdded
        };
    }
}