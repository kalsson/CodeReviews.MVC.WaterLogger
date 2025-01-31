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
            try
            {
                Book = _bookRepository.GetBookById(id);

                if (Book == null)
                {
                    return RedirectToPage("/Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _bookRepository.DeleteBook(id);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                ModelState.AddModelError(string.Empty, "An error occurred while trying to delete the book. Please try again.");

                return Page();
            }
        }
    }
}
