using LIBRARYMANAGEMENT.API.DTOs;

namespace LIBRARYMANAGEMENT.API.Services;

public interface IBooksService
{
    Task<List<BookDto>> GetAllBooksAsync(string? category = null, string? author = null, string? title = null);
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(CreateBookDto newBook);
    Task<bool> UpdateBookAsync(int id, UpdateBookDto updatedBook);
    Task<bool> DeleteBookAsync(int id);
}