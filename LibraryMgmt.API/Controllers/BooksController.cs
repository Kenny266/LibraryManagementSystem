using LibraryMgmt.API.DTOs;
using LibraryMgmt.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;
    private readonly ILoansService _loansService;

    public BooksController(IBooksService booksService, ILoansService loansService)
    {
        _booksService = booksService;
        _loansService = loansService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetBooks(
        [FromQuery] string? category,
        [FromQuery] string? author,
        [FromQuery] string? title)
    {
        var books = await _booksService.GetAllBooksAsync(category, author, title);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _booksService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpGet("{id}/loans")]
    public async Task<ActionResult<List<LoanDto>>> GetBookLoans(int id)
    {
        var book = await _booksService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound("Book not found");
        }

        var allLoans = await _loansService.GetAllLoansAsync();
        var bookLoans = allLoans.Where(l => l.BookId == id).ToList();
        return Ok(bookLoans);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto newBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdBook = await _booksService.CreateBookAsync(newBook);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBook(int id, UpdateBookDto updatedBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _booksService.UpdateBookAsync(id, updatedBook);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        var success = await _booksService.DeleteBookAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
