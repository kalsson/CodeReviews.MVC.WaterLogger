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

    public void OnGet()
    {
        try
        {
            Books = _bookRepository.GetAllBooks();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            
            Books = new List<Book>();
        }
    }
}