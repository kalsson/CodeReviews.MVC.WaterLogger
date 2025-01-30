using BookReadingTracker.UI.DataAccess;
using BookReadingTracker.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookReadingTracker.UI.Pages
{
    public class CreateModel : PageModel
    {
        private readonly BookRepository _bookRepository;

        public CreateModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _bookRepository.AddBook(Book);
            return RedirectToPage("/Index");
        }

    }
}
