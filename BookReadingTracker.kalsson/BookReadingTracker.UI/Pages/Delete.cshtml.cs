using BookReadingTracker.UI.DataAccess;
using BookReadingTracker.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookReadingTracker.UI.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly BookRepository _bookRepository;

        public DeleteModel(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Book = _bookRepository.GetBookById(id);

            if (Book == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _bookRepository.DeleteBook(id);

            return RedirectToPage("/Index");
        }

    }
}
