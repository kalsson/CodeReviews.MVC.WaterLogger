using BookReadingTracker.UI.DataAccess;
using BookReadingTracker.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookReadingTracker.UI.Pages;

public class IndexModel : PageModel
{
    private readonly BookRepository _bookRepository;

    public IndexModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> Books { get; set; } = new();

    // Pagination properties
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public const int PageSize = 8; // Number of records per page

    public void OnGet(int currentPage = 1)
    {
        try
        {
            var allBooks = _bookRepository.GetAllBooks(); // Fetch all books
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(allBooks.Count / (double)PageSize);

            // Fetch books only for the current page using LINQ
            Books = allBooks
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Books = new List<Book>();
        }
    }

}